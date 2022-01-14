using Cofoundry.Domain.Internal;
using Dot.Web.Admin.Domain.Setup.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.BasicTestSite
{
    public class IsSetupMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SettingQueryHelper _getSettingQueryHelper;
        private readonly IInternalSettingsRepository _internalSettingsRepository;

        public IsSetupMiddleware(
            RequestDelegate next,
            SettingQueryHelper getSettingQueryHelper,
            IInternalSettingsRepository internalSettingsRepository)
        {
            _next = next;
            _getSettingQueryHelper = getSettingQueryHelper;
            _internalSettingsRepository = internalSettingsRepository;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var allSettings = await _internalSettingsRepository.GetAllSettingsAsync();
            var setting = MapSettings(allSettings);

            await _next(httpContext);
            return;

            var isSetupRoute = httpContext.Request.Path.StartsWithSegments(new PathString("/setup"));

            if (!setting.IsSetup)
            {
                if (isSetupRoute)
                {
                    var headers = httpContext.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(0)
                    };

                    await _next(httpContext);
                    return;
                }

                httpContext.Response.Redirect("/setup", true);
                return;
            }

            if (isSetupRoute)
            {
                httpContext.Response.Redirect("/", true);
                return;
            }

            await _next(httpContext);
        }

        private InternalSettings MapSettings(Dictionary<string, string> allSettings)
        {
            var settings = new InternalSettings();

            _getSettingQueryHelper.SetSettingProperty(settings, s => s.IsSetup, allSettings);

            return settings;
        }
    }
}

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;

namespace Cofoundry.Web
{
    /// <summary>
    /// The default auth configuration adds cookie auth for each of the user areas.
    /// </summary>
    public class DefaultAuthConfiguration : IAuthConfiguration
    {
        private readonly IAuthCookieNamespaceProvider _authCookieNamespaceProvider;

        public DefaultAuthConfiguration(
            IAuthCookieNamespaceProvider authCookieNamespaceProvider
            )
        {
            _authCookieNamespaceProvider = authCookieNamespaceProvider;
        }

        public void Configure(IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            var authBuilder = mvcBuilder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            var cookieNamespace = _authCookieNamespaceProvider.GetNamespace();

            authBuilder
                .AddCookie(options =>
                {
                    options.Cookie.Name = cookieNamespace;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;

                    options.Events.OnRedirectToLogin = context =>
                   {
                       context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                       return Task.CompletedTask;
                   };

                    options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return Task.CompletedTask;
                    };
                });

            mvcBuilder.Services.AddAuthorization();
        }
    }
}

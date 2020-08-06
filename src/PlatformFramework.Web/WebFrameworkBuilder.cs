using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Web.Authorization;

namespace PlatformFramework.Web
{
    /// <summary>
    /// Configure PlatformFramework.Web services
    /// </summary>
    public class WebFrameworkBuilder
    {
        private IServiceCollection Services { get; }

        internal WebFrameworkBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public WebFrameworkBuilder WithPermissionAuthorization()
        {
            Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            return this;
        }

        public WebFrameworkBuilder WithResponseCompression(Action<ResponseCompressionOptions> setupAction)
        {
            Services.AddResponseCompression(setupAction);
            return this;
        }

        public WebFrameworkBuilder WithCors(Action<CorsOptions> setupAction)
        {
            Services.AddCors(setupAction);
            return this;
        }
    }
}
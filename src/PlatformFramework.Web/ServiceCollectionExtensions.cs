using System;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Web.Runtime;
using PlatformFramework.Interfaces.Runtime;

namespace PlatformFramework.Web
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Web Framework features
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <returns><see cref="WebFrameworkBuilder"/></returns>
        public static WebFrameworkBuilder AddWebFramework(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddHttpContextAccessor();
            services.AddScoped<IUserSession, UserSession>();

            return new WebFrameworkBuilder(services);
        }
    }
}
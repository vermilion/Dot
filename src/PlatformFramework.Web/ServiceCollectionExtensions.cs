using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Abstractions;
using PlatformFramework.Web.Runtime;

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
            Guard.Against.Null(services, nameof(services));

            services.AddHttpContextAccessor();
            services.AddScoped<IUserSession, UserSession>();

            return new WebFrameworkBuilder(services);
        }
    }
}
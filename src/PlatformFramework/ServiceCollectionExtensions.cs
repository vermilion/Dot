using System;
using Microsoft.Extensions.DependencyInjection;

namespace PlatformFramework
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add default Framework features
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configure">Configure action for <see cref="FrameworkOptions"/></param>
        /// <returns><see cref="FrameworkBuilder"/></returns>
        public static FrameworkBuilder AddFramework(this IServiceCollection services, Action<FrameworkOptions>? configure = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return new FrameworkBuilder(services, configure);
        }
    }
}
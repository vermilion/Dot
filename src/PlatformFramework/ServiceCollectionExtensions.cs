using System;
using Ardalis.GuardClauses;
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
            Guard.Against.Null(services, nameof(services));

            return new FrameworkBuilder(services, configure);
        }
    }
}
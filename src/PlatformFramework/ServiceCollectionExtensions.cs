using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PlatformFramework
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Framework feature modules
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configure">Configure action for <see cref="FrameworkOptions"/></param>
        public static void AddFramework(this IServiceCollection services, Action<FrameworkOptions>? configure = null)
        {
            Guard.Against.Null(services, nameof(services));

            var options = new FrameworkOptions();
            configure?.Invoke(options);

            // add default framework module
            options.AddModule<FrameworkModule>();

            // register services per each module
            foreach (var module in options.Modules)
            {
                module.ConfigureServices(services);
            }
        }
    }
}
using System.Collections.Generic;
using Cofoundry.Core.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cofoundry.Web
{
    public static class AddDotStartupExtension
    {
        /// <summary>
        /// Configures the dependency resolver and registers all the services, repositories and modules setup for auto-registration.
        /// </summary>
        public static IMvcBuilder AddDot<TStartup>(this IMvcBuilder mvcBuilder, IConfiguration configuration)
            where TStartup : IDotStartup, new()
        {
            var builder = new DotStartupBuilder();
            var startup = new TStartup();
            startup.Configure(builder);

            mvcBuilder = EnsureCoreMVCServicesAdded(mvcBuilder);

            AddAdditionalTypes(mvcBuilder);

            var typesProvider = new DiscoveredTypesProvider(mvcBuilder.PartManager);
            var containerBuilder = new DefaultContainerBuilder(mvcBuilder.Services, typesProvider, configuration);
            containerBuilder.Build();

            RunAdditionalConfiguration(mvcBuilder);

            return mvcBuilder;
        }

        /// <summary>
        /// Ensure the correct component parts required to run Cofoundry have been added
        /// </summary>
        private static IMvcBuilder EnsureCoreMVCServicesAdded(IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.Services
                .AddControllersWithViews()
                ;
        }

        private static void AddAdditionalTypes(IMvcBuilder mvcBuilder)
        {
            // Ensure IHttpContextAccessor is added, because it isn't by default
            // see https://github.com/aspnet/Hosting/issues/793
            mvcBuilder.Services.AddHttpContextAccessor();
        }

        /// <summary>
        /// MVC does not do a very good job of modular configurations, so here
        /// we have to prematurely build the container and use a child scope to 
        /// run additional configurations based on what has already been setup in the
        /// DI container. This allows for additional configuration to be made in
        /// plugins.
        /// </summary>
        private static void RunAdditionalConfiguration(IMvcBuilder mvcBuilder)
        {
            var serviceProvider = mvcBuilder.Services.BuildServiceProvider();
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var mvcBuilderConfigurations = serviceScope.ServiceProvider.GetRequiredService<IEnumerable<IStartupServiceConfigurationTask>>();
                foreach (var mvcBuilderConfiguration in mvcBuilderConfigurations)
                {
                    mvcBuilderConfiguration.ConfigureServices(mvcBuilder);
                }
            }
        }
    }
}
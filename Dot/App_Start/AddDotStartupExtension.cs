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
        public static IServiceCollection AddDot<TStartup>(this IServiceCollection services, IConfiguration configuration)
            where TStartup : IDotStartup, new()
        {
            var builder = new DotStartupBuilder();
            var startup = new TStartup();
            startup.Configure(builder);
            
            var containerBuilder = new DefaultContainerBuilder(services, configuration);
            containerBuilder.Build(builder.Registrations);

            RunAdditionalConfiguration(services);

            return services;
        }

        /// <summary>
        /// MVC does not do a very good job of modular configurations, so here
        /// we have to prematurely build the container and use a child scope to 
        /// run additional configurations based on what has already been setup in the
        /// DI container. This allows for additional configuration to be made in
        /// plugins.
        /// </summary>
        private static void RunAdditionalConfiguration(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var serviceScope = serviceProvider.CreateScope())
            {
                var mvcBuilderConfigurations = serviceScope.ServiceProvider.GetRequiredService<IEnumerable<IStartupServiceConfigurationTask>>();
                foreach (var mvcBuilderConfiguration in mvcBuilderConfigurations)
                {
                    mvcBuilderConfiguration.ConfigureServices(services);
                }
            }
        }
    }
}
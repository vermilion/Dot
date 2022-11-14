using Microsoft.Extensions.DependencyInjection;

namespace Cofoundry.Web
{
    /// <summary>
    /// Extends the IMvcBuilder configuration to allow for modular configuration
    /// of Mvc services
    /// </summary>
    public class CofoundryStartupServiceConfigurationTask : IStartupServiceConfigurationTask
    {
        private readonly IAuthConfiguration _authConfiguration;

        public CofoundryStartupServiceConfigurationTask(
            IAuthConfiguration authConfiguration
            )
        {
            _authConfiguration = authConfiguration;
        }

        /// <summary>
        /// Configures Mvc services. Runs after AddMvc in the service
        /// configuration pipeline.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            _authConfiguration.Configure(services);
        }
    }
}

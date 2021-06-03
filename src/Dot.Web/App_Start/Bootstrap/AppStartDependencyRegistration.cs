using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Web.Registration
{
    public class AppStartDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .RegisterAll<IStartupServiceConfigurationTask>()
                .RegisterAll<IStartupConfigurationTask>()
                .Register<IAuthConfiguration, DefaultAuthConfiguration>()
                .RegisterSingleton<AutoUpdateState>()
            ;
        }
    }
}

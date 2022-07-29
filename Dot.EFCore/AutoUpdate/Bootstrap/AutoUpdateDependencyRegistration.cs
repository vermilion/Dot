using Cofoundry.Core.DependencyInjection;
using Cofoundry.Web;
using Dot.Configuration.Extensions;
using Dot.EFCore.AutoUpdate.Services;
using Dot.EFCore.AutoUpdate.Services.Interfaces;

namespace Cofoundry.Core.AutoUpdate.Registration
{
    public class AutoUpdateDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.Settings(x =>
            {
                x.Register<AutoUpdateSettings>();
                x.Register<DatabaseSettings>();
            });

            container.Startup(x =>
            {
                x.RegisterServiceConfigurationTask<AutoUpdateServiceConfigurationTask>();
            });

            // TODO: register all IVersionedUpdateCommandHandler<>
            // TODO: register all IAlwaysRunUpdateCommandHandler<>

            container
                .Register<IUpdateCommandHandlerFactory, UpdateCommandHandlerFactory>()
                .Register<IAutoUpdateService, AutoUpdateService>()
                .RegisterSingleton<AutoUpdateState>();
        }
    }
}

using Cofoundry.Core.AutoUpdate.Internal;
using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Core.AutoUpdate.Registration
{
    public class AutoUpdateDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .RegisterAllGenericImplementations(typeof(IVersionedUpdateCommandHandler<>))
                .RegisterAllGenericImplementations(typeof(IAlwaysRunUpdateCommandHandler<>))
                .Register<IUpdateCommandHandlerFactory, UpdateCommandHandlerFactory>()
                .Register<IAutoUpdateService, AutoUpdateService>()
                .Register<IUpdatePackageOrderer, UpdatePackageOrderer>()
                .Register<IAutoUpdateDistributedLockManager, AutoUpdateDistributedLockManager>()
                .RegisterAll<IUpdatePackageFactory>()
                ;
        }
    }
}

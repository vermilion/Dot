using Cofoundry.Core.AutoUpdate.Internal;
using Cofoundry.Core.DependencyInjection;
using PlatformFramework.EFCore.Context.Migrations;

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
                .Register<IDbHealthChecker, DbHealthChecker>()
                .Register<IAutoUpdateDistributedLockManager, AutoUpdateDistributedLockManager>()
                ;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Internal;

namespace Cofoundry.Domain.Registration
{
    public class RolesDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            var singletonOptions = RegistrationOptions.SingletonScope();

            // TODO: register all IPermission with singletonOptions
            // TODO: register all IRoleDefinition with singletonOptions
            // TODO: register all IRoleInitializer<>

            container
                .Register<IPermissionValidationService, PermissionValidationService>()
                .Register<IExecutePermissionValidationService, ExecutePermissionValidationService>()
                .Register<IRoleCache, RoleCache>()
                .Register<IInternalRoleRepository, InternalRoleRepository>()
                .Register<IRoleDetailsMapper, RoleDetailsMapper>()
                .Register<IRoleMicroSummaryMapper, RoleMicroSummaryMapper>()
                .RegisterSingleton<IPermissionRepository, PermissionRepository>()

                .Register<IRoleInitializerFactory, RoleInitializerFactory>()
                ;
        }
    }
}

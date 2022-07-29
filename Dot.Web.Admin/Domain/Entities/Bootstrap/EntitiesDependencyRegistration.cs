using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain.Internal;

namespace Cofoundry.Domain.Registration
{
    public class EntitiesDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            // TODO: register all IEntityDefinition with RegistrationOptions.SingletonScope()

            container
                .RegisterSingleton<IEntityDefinitionRepository, EntityDefinitionRepository>();
        }
    }
}

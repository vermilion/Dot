using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.Validation.Internal;

namespace Cofoundry.Core.Validation.Registration
{
    public class FluentValidationDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            var normalPriorityOverrideRegistrationOptions = RegistrationOptions.Override(RegistrationOverridePriority.Normal);

            container
                .Register<IModelValidationService, FluentModelValidationService>(normalPriorityOverrideRegistrationOptions)
                ;
        }
    }
}

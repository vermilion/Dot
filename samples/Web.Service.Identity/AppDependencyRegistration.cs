using Cofoundry.BasicTestSite;
using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.EntityFramework;

namespace Cofoundry.Core.Validation.Registration
{
    public class AppDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            var highPriorityOverrideRegistrationOptions = RegistrationOptions.Override(RegistrationOverridePriority.High);

            container
                .Register<IDbContextInitializer, AppDbContextInitializer>(highPriorityOverrideRegistrationOptions)
                ;
        }
    }
}

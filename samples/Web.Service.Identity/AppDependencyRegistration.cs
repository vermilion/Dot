using Cofoundry.BasicTestSite;
using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain.Data;
using Dot.Configuration.Extensions;

namespace Cofoundry.Core.Validation.Registration
{
    public class AppDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            var highPriorityOverrideRegistrationOptions = RegistrationOptions.Override(RegistrationOverridePriority.High);

            container.Settings(x =>
            {
                x.Register<BasicTestSiteSettings>();
            });

            container
                .Register<DbContextCore, AppDbContext>(highPriorityOverrideRegistrationOptions)
                ;
        }
    }
}

using Cofoundry.Core.DependencyInjection;
using Dot.EFCore.Health.Services;

namespace Cofoundry.Core.Health.Registration
{
    public class HealthDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .Register<IDbHealthChecker, DbHealthChecker>();
        }
    }
}

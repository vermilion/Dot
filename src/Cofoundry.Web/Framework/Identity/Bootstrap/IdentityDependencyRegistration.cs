using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Web.Identity.Registration
{
    public class IdentityDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .Register<AccountManagementControllerHelper, AccountManagementControllerHelper>()
                .Register<AuthenticationControllerHelper, AuthenticationControllerHelper>()
                .Register<UserManagementControllerHelper, UserManagementControllerHelper>()
                ;

        }
    }
}

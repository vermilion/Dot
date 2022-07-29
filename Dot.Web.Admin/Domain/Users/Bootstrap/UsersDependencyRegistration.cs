using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain.Internal;
using Dot.Configuration.Extensions;

namespace Cofoundry.Domain.Registration
{
    public class UsersDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.Settings(x =>
            {
                x.Register<AuthenticationSettings>();
            });

            container
                .Register<UserContextMapper>()
                .Register<IResetUserPasswordCommandHelper, ResetUserPasswordCommandHelper>()
                .Register<IPasswordUpdateCommandHelper, PasswordUpdateCommandHelper>()
                .Register<IUserContextService, UserContextService>(RegistrationOptions.Scoped())
                .Register<IUserMicroSummaryMapper, UserMicroSummaryMapper>()
                .Register<IUserSummaryMapper, UserSummaryMapper>()
                .Register<IUserAccountDetailsMapper, UserAccountDetailsMapper>()
                .Register<IUserDetailsMapper, UserDetailsMapper>()
                .Register<UserAuthenticationHelper, UserAuthenticationHelper>()
                .Register<UserCommandPermissionsHelper, UserCommandPermissionsHelper>()
                ;
        }
    }
}

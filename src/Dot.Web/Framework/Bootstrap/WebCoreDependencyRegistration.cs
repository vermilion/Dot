using Cofoundry.Core;
using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain;

namespace Cofoundry.Web.Registration
{
    public class WebCoreDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            var lowPriorityOverrideRegistrationOptions = RegistrationOptions.Override(RegistrationOverridePriority.Low);

            container
                .Register<IUserSessionService, UserSessionService>(RegistrationOptions.Scoped())
                .Register<IAuthCookieNamespaceProvider, AuthCookieNamespaceProvider>()

                .Register<IPathResolver, SitePathResolver>(lowPriorityOverrideRegistrationOptions)
                ;
        }
    }
}

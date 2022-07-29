using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain;
using Dot.Configuration.Extensions;
using Dot.Parts.Mail.Core;

namespace Cofoundry.Web.Registration
{
    public class WebCoreDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            var lowPriorityOverrideRegistrationOptions = RegistrationOptions.Override(RegistrationOverridePriority.Low);

            container.Startup(x =>
            {
                x.RegisterServiceConfigurationTask<CofoundryStartupServiceConfigurationTask>();
            });

            container
                .Register<IUserSessionService, UserSessionService>(RegistrationOptions.Scoped())
                .Register<IAuthCookieNamespaceProvider, AuthCookieNamespaceProvider>()
                .Register<IAuthConfiguration, DefaultAuthConfiguration>()
                .Register<IPathResolver, SitePathResolver>(lowPriorityOverrideRegistrationOptions)
                ;
        }
    }
}

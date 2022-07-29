using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.Mail.Internal;
using Dot.Configuration.Extensions;

namespace Cofoundry.Core.Mail.Registration
{
    public class MailDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.Settings(x =>
            {
                x.Register<MailSettings>();
            });

            container
                .Register<IMailService, SimpleMailService>()
                .Register<IMailMessageRenderer, MailMessageRenderer>()
                .Register<IMailViewRenderer, RazorMailViewRenderer>()
                .Register<IMailDispatchService, DebugMailDispatchService>()
                ;
        }
    }
}

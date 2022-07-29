using Cofoundry.Domain.MailTemplates;

namespace Cofoundry.Domain
{
    public interface IResetUserPasswordCommand
    {
        IResetPasswordTemplate MailTemplate { get; set; }
    }
}

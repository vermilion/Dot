using System.ComponentModel.DataAnnotations;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.MailTemplates;

namespace Cofoundry.Domain
{
    public class ResetUserPasswordByUsernameCommand : IRequest<Unit>, ILoggableCommand, IResetUserPasswordCommand
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public IResetPasswordTemplate MailTemplate { get; set; }
    }
}

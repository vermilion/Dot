using System.ComponentModel.DataAnnotations;
using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.MailTemplates;

namespace Cofoundry.Domain
{
    public class ResetUserPasswordByUserIdCommand : IRequest<Unit>, ILoggableCommand, IResetUserPasswordCommand
    {
        [Required]
        [PositiveInteger]
        public int UserId { get; set; }

        [Required]
        public IResetPasswordTemplate MailTemplate { get; set; }
    }
}

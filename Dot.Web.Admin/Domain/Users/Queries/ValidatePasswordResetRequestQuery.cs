using Cofoundry.Domain.CQS;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Domain
{
    public class ValidatePasswordResetRequestQuery : IRequest<PasswordResetRequestAuthenticationResult>
    {
        [Required]
        public Guid UserPasswordResetRequestId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}

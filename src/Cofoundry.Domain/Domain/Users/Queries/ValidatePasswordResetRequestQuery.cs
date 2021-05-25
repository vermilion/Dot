using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;

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

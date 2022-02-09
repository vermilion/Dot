using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Core.Validation;

namespace Cofoundry.Domain
{
    public class LogFailedLoginAttemptCommand : IRequest<Unit>
    {
        public LogFailedLoginAttemptCommand()
        {
        }

        public LogFailedLoginAttemptCommand(string username)
        {
            Username = username;
        }

        [Required]
        public string Username { get; set; }
    }
}

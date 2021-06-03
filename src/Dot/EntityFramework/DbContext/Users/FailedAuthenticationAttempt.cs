using System;

namespace Cofoundry.Domain.Data
{
    public partial class FailedAuthenticationAttempt
    {
        public string UserName { get; set; }

        public string IPAddress { get; set; }

        public DateTime AttemptDate { get; set; }

        public int FailedAuthenticationAttemptId { get; set; }
    }
}

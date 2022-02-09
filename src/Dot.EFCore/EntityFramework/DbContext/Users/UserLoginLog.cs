using System;

namespace Cofoundry.Domain.Data
{
    public partial class UserLoginLog
    {
        public int UserId { get; set; }

        public string IPAddress { get; set; }

        public DateTime AttemptDate { get; set; }

        public int UserLoginLogId { get; set; }
    }
}

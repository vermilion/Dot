using System;

namespace Cofoundry.Core.AutoUpdate
{
    public class AutoUpdateProcessLockedException : Exception
    {
        public AutoUpdateProcessLockedException()
            : base("The auto-update process cannot be started because it has been locked by another machine")
        {
        }
    }
}

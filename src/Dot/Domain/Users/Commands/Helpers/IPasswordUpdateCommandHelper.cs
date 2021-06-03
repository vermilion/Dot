using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Helper used by in password update commands for shared functionality.
    /// </summary>
    public interface IPasswordUpdateCommandHelper
    {
        void ValidatePermissions(IExecutionContext executionContext);

        void UpdatePassword(string newPassword, User user, IExecutionContext executionContext);
    }
}

using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Helper used by ResetUserPasswordByUserIdCommandHandler and ResetUserPasswordByUsernameCommandHandler
    /// for shared functionality.
    /// </summary>
    public interface IResetUserPasswordCommandHelper
    {
        #region public methods

        Task ValidateCommandAsync(IResetUserPasswordCommand command, IExecutionContext executionContext);
        
        Task ResetPasswordAsync(User user, IResetUserPasswordCommand command, IExecutionContext executionContext);

        #endregion
    }
}

using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Helper used by in password update commands for shared functionality.
    /// </summary>
    public class PasswordUpdateCommandHelper : IPasswordUpdateCommandHelper
    {
        private readonly IPermissionValidationService _permissionValidationService;
        private readonly IPasswordCryptographyService _passwordCryptographyService;

        public PasswordUpdateCommandHelper(
            IPermissionValidationService permissionValidationService,
            IPasswordCryptographyService passwordCryptographyService
            )
        {
            _passwordCryptographyService = passwordCryptographyService;
            _permissionValidationService = permissionValidationService;
        }

        public void ValidatePermissions(IExecutionContext executionContext)
        {
            _permissionValidationService.EnforcePermission(new CofoundryUserUpdatePermission(), executionContext.UserContext);
        }

        public void UpdatePassword(string newPassword, User user, IExecutionContext executionContext)
        {
            user.RequirePasswordChange = false;
            user.LastPasswordChangeDate = executionContext.ExecutionDate;

            var hashResult = _passwordCryptographyService.CreateHash(newPassword);
            user.Password = hashResult.Hash;
        }
    }
}

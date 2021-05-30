using Cofoundry.Core;
using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Updates the password of the currently logged in user, using the
    /// OldPassword field to authenticate the request.
    /// </summary>
    public class UpdateCurrentUserPasswordCommandHandler 
        : IRequestHandler<UpdateCurrentUserPasswordCommand, Unit>
        , IPermissionRestrictedRequestHandler<UpdateCurrentUserPasswordCommand>
    {
        #region constructor

        private readonly DbContextCore _dbContext;
        private readonly UserAuthenticationHelper _userAuthenticationHelper;
        private readonly IPermissionValidationService _permissionValidationService;
        private readonly IPasswordUpdateCommandHelper _passwordUpdateCommandHelper;

        public UpdateCurrentUserPasswordCommandHandler(
            DbContextCore dbContext,
            UserAuthenticationHelper userAuthenticationHelper,
            IPermissionValidationService permissionValidationService,
            IPasswordUpdateCommandHelper passwordUpdateCommandHelper
            )
        {
            _dbContext = dbContext;
            _userAuthenticationHelper = userAuthenticationHelper;
            _permissionValidationService = permissionValidationService;
            _passwordUpdateCommandHelper = passwordUpdateCommandHelper;
        }

        #endregion

        #region execution
        
        public async Task<Unit> ExecuteAsync(UpdateCurrentUserPasswordCommand command, IExecutionContext executionContext)
        {
            var user = await GetUser(command, executionContext);
            UpdatePassword(command, executionContext, user);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private void UpdatePassword(UpdateCurrentUserPasswordCommand command, IExecutionContext executionContext, User user)
        {
            EntityNotFoundException.ThrowIfNull(user, executionContext.UserContext.UserId);

            if (!_userAuthenticationHelper.IsPasswordCorrect(user, command.OldPassword))
            {
                throw ValidationErrorException.CreateWithProperties("Incorrect password", "OldPassword");
            }

            _passwordUpdateCommandHelper.UpdatePassword(command.NewPassword, user, executionContext);
        }

        private Task<User> GetUser(UpdateCurrentUserPasswordCommand command, IExecutionContext executionContext)
        {
            return _dbContext
                .Users
                .FilterCanLogIn()
                .FilterById(executionContext.UserContext.UserId.Value)
                .SingleOrDefaultAsync();
        }

        #endregion

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(UpdateCurrentUserPasswordCommand command)
        {
            yield return new CurrentUserUpdatePermission();
        }

        #endregion
    }
}

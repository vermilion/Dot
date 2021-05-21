using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    public class ResetUserPasswordByUserIdCommandHandler 
        : ICommandHandler<ResetUserPasswordByUserIdCommand>
    {
        #region construstor

        private readonly CofoundryDbContext _dbContext;
        private readonly IResetUserPasswordCommandHelper _resetUserPasswordCommandHelper;
        private readonly IPermissionValidationService _permissionValidationService;
        
        public ResetUserPasswordByUserIdCommandHandler(
            CofoundryDbContext dbContext,
            IResetUserPasswordCommandHelper resetUserPasswordCommandHelper,
            IPermissionValidationService permissionValidationService
            )
        {
            _dbContext = dbContext;
            _resetUserPasswordCommandHelper = resetUserPasswordCommandHelper; 
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        #region execution

        public async Task ExecuteAsync(ResetUserPasswordByUserIdCommand command, IExecutionContext executionContext)
        {
            await _resetUserPasswordCommandHelper.ValidateCommandAsync(command, executionContext);
            var user = await QueryUser(command, executionContext).SingleOrDefaultAsync();
            ValidatePermissions(user, executionContext);
            await _resetUserPasswordCommandHelper.ResetPasswordAsync(user, command, executionContext);
        }

        #endregion

        #region private helpers

        private IQueryable<User> QueryUser(ResetUserPasswordByUserIdCommand command, IExecutionContext executionContext)
        {
            var user = _dbContext
                .Users
                .FilterById(command.UserId)
                .FilterCanLogIn();

            return user;
        }

        #endregion

        #region Permission

        public void ValidatePermissions(User user, IExecutionContext executionContext)
        {
            _permissionValidationService.EnforcePermission(new CofoundryUserUpdatePermission(), executionContext.UserContext);
        }

        #endregion
    }
}

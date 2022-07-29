using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;
using Dot.EFCore.UnitOfWork;

namespace Cofoundry.Domain.Internal
{
    public class ResetUserPasswordByUserIdCommandHandler 
        : IRequestHandler<ResetUserPasswordByUserIdCommand, Unit>
    {
        #region construstor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IResetUserPasswordCommandHelper _resetUserPasswordCommandHelper;
        private readonly IPermissionValidationService _permissionValidationService;
        
        public ResetUserPasswordByUserIdCommandHandler(
            IUnitOfWork unitOfWork,
            IResetUserPasswordCommandHelper resetUserPasswordCommandHelper,
            IPermissionValidationService permissionValidationService
            )
        {
            _unitOfWork = unitOfWork;
            _resetUserPasswordCommandHelper = resetUserPasswordCommandHelper; 
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        #region execution

        public async Task<Unit> ExecuteAsync(ResetUserPasswordByUserIdCommand command, IExecutionContext executionContext)
        {
            await _resetUserPasswordCommandHelper.ValidateCommandAsync(command, executionContext);
            var user = await QueryUser(command, executionContext).SingleOrDefaultAsync();
            ValidatePermissions(user, executionContext);
            await _resetUserPasswordCommandHelper.ResetPasswordAsync(user, command, executionContext);

            return Unit.Value;
        }

        #endregion

        #region private helpers

        private IQueryable<User> QueryUser(ResetUserPasswordByUserIdCommand command, IExecutionContext executionContext)
        {
            var user = _unitOfWork
                .Users()
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

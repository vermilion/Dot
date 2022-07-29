using Cofoundry.Core;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Updates the user account of the currently logged in user.
    /// </summary>
    public class UpdateCurrentUserAccountCommandHandler 
        : IRequestHandler<UpdateCurrentUserAccountCommand, Unit>
        , IPermissionRestrictedRequestHandler<UpdateCurrentUserAccountCommand>
    {
        #region consructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _queryExecutor;
        private readonly IPermissionValidationService _permissionValidationService;

        public UpdateCurrentUserAccountCommandHandler(
            IMediator queryExecutor,
            IUnitOfWork unitOfWork,
            IPermissionValidationService permissionValidationService
            )
        {
            _queryExecutor = queryExecutor;
            _unitOfWork = unitOfWork;
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        #region execution

        public async Task<Unit> ExecuteAsync(UpdateCurrentUserAccountCommand command, IExecutionContext executionContext)
        {
            var userId = executionContext.UserContext.UserId.Value;

            var user = await _unitOfWork
                .Users()
                .FilterCanLogIn()
                .FilterById(userId)
                .SingleOrDefaultAsync();

            EntityNotFoundException.ThrowIfNull(user, userId);

            user.Email = command.Email?.Trim();
            user.FirstName = command.FirstName.Trim();
            user.LastName = command.LastName.Trim();

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }

        #endregion

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(UpdateCurrentUserAccountCommand command)
        {
            yield return new CurrentUserUpdatePermission();
        }

        #endregion
    }
}

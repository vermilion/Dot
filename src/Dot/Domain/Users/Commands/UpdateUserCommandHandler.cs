using Cofoundry.Core;
using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, Unit>
    {
        #region constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _queryExecutor;
        private readonly UserCommandPermissionsHelper _userCommandPermissionsHelper;
        private readonly IPermissionValidationService _permissionValidationService;

        public UpdateUserCommandHandler(
            IMediator queryExecutor,
            IUnitOfWork unitOfWork,
            UserCommandPermissionsHelper userCommandPermissionsHelper,
            IPermissionValidationService permissionValidationService
            )
        {
            _queryExecutor = queryExecutor;
            _unitOfWork = unitOfWork;
            _userCommandPermissionsHelper = userCommandPermissionsHelper;
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        #region Execution

        public async Task<Unit> ExecuteAsync(UpdateUserCommand command, IExecutionContext executionContext)
        {
            // Get User
            var user = await _unitOfWork
                .Users()
                .FilterCanLogIn()
                .FilterById(command.UserId)
                .SingleOrDefaultAsync();

            EntityNotFoundException.ThrowIfNull(user, command.UserId);

            // Validate
            ValidatePermissions(executionContext);
            ValidateCommand(command);
            await ValidateIsUniqueAsync(command, executionContext);

            // Role
            if (command.RoleId != user.RoleId)
            {
                user.Role = await _userCommandPermissionsHelper.GetAndValidateNewRoleAsync(
                    command.RoleId,
                    user.RoleId,
                    executionContext
                    );
            }

            // Map
            Map(command, user);

            // Save
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }

        private static void Map(UpdateUserCommand command, User user)
        {
            user.FirstName = command.FirstName?.Trim();
            user.LastName = command.LastName?.Trim();
            user.Email = command.Email?.Trim();
            user.Username = command.Username?.Trim();

            user.RequirePasswordChange = command.RequirePasswordChange;
        }

        private void ValidateCommand(UpdateUserCommand command)
        {
            // Username
            if (string.IsNullOrWhiteSpace(command.Username))
            {
                throw ValidationErrorException.CreateWithProperties("Username field is required", "Username");
            }
        }

        private async Task ValidateIsUniqueAsync(
            UpdateUserCommand command,
            IExecutionContext executionContext
            )
        {
            var query = new IsUsernameUniqueQuery()
            {
                UserId = command.UserId,
            };

            query.Username = command.Username.Trim();

            var isUnique = await _queryExecutor.ExecuteAsync(query, executionContext);

            if (!isUnique)
            {
                throw ValidationErrorException.CreateWithProperties("This username is already registered", "Username");
            }
        }

        #endregion

        #region Permission

        public void ValidatePermissions(IExecutionContext executionContext)
        {
            _permissionValidationService.EnforcePermission(new CofoundryUserUpdatePermission(), executionContext.UserContext);
        }

        #endregion
    }
}

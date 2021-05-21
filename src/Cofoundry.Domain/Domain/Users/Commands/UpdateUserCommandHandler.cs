using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;
using Cofoundry.Core.Validation;
using Cofoundry.Core;

namespace Cofoundry.Domain.Internal
{
    public class UpdateUserCommandHandler
        : ICommandHandler<UpdateUserCommand>
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IQueryExecutor _queryExecutor;
        private readonly UserCommandPermissionsHelper _userCommandPermissionsHelper;
        private readonly IPermissionValidationService _permissionValidationService;

        public UpdateUserCommandHandler(
            IQueryExecutor queryExecutor,
            CofoundryDbContext dbContext,
            UserCommandPermissionsHelper userCommandPermissionsHelper,
            IPermissionValidationService permissionValidationService
            )
        {
            _queryExecutor = queryExecutor;
            _dbContext = dbContext;
            _userCommandPermissionsHelper = userCommandPermissionsHelper;
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        #region Execution

        public async Task ExecuteAsync(UpdateUserCommand command, IExecutionContext executionContext)
        {
            // Get User
            var user = await _dbContext
                .Users
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
            await _dbContext.SaveChangesAsync();
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
            if (!string.IsNullOrWhiteSpace(command.Username))
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

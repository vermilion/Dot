using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.Transactions.Services.Interfaces;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Deletes a role with the specified database id. Roles cannot be
    /// deleted if assigned to users.
    /// </summary>
    public class DeleteRoleCommandHandler 
        : IRequestHandler<DeleteRoleCommand, Unit>
        , IPermissionRestrictedRequestHandler<DeleteRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        #region constructor

        private readonly IRoleCache _roleCache;
        private readonly ITransactionScopeManager _transactionScopeFactory;

        public DeleteRoleCommandHandler(
            IUnitOfWork unitOfWork,
            DbContextCore dbContext,
            UserCommandPermissionsHelper userCommandPermissionsHelper,
            IRoleCache roleCache,
            ITransactionScopeManager transactionScopeFactory
            )
        {
            _unitOfWork = unitOfWork;
            _roleCache = roleCache;
            _transactionScopeFactory = transactionScopeFactory;
        }

        #endregion

        #region execution

        public async Task<Unit> ExecuteAsync(DeleteRoleCommand command, IExecutionContext executionContext)
        {
            var role = await _unitOfWork
                .Roles()
                .FilterById(command.RoleId)
                .SingleOrDefaultAsync();

            if (role != null)
            {
                ValidateCanDelete(role, command);

                _unitOfWork.Roles().Remove(role);

                await _unitOfWork.SaveChangesAsync();
                await _transactionScopeFactory.QueueCompletionTaskAsync(_unitOfWork.Context, () => Task.Run(() => _roleCache.Clear(command.RoleId)));
            }

            return Unit.Value;
        }

        private void ValidateCanDelete(Role role, DeleteRoleCommand command)
        {
            if (role.RoleCode == AnonymousRole.AnonymousRoleCode)
            {
                throw new ValidationException("The anonymous role cannot be deleted.");
            }

            if (role.RoleCode == SuperAdminRole.SuperAdminRoleCode)
            {
                throw new ValidationException("The super administrator role cannot be deleted.");
            }

            var isInUse = _unitOfWork
                .Users()
                .Any(u => u.RoleId == command.RoleId);

            if (isInUse)
            {
                throw new ValidationException("Role is in use and cannot be deleted.");
            }
        }

        #endregion

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(DeleteRoleCommand command)
        {
            yield return new RoleDeletePermission();
        }

        #endregion
    }
}

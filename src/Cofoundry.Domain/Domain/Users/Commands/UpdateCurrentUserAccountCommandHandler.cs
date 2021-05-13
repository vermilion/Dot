using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;
using Cofoundry.Core;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Updates the user account of the currently logged in user.
    /// </summary>
    public class UpdateCurrentUserAccountCommandHandler 
        : ICommandHandler<UpdateCurrentUserAccountCommand>
        , IPermissionRestrictedCommandHandler<UpdateCurrentUserAccountCommand>
    {
        #region consructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IPermissionValidationService _permissionValidationService;

        public UpdateCurrentUserAccountCommandHandler(
            IQueryExecutor queryExecutor,
            CofoundryDbContext dbContext,
            IPermissionValidationService permissionValidationService
            )
        {
            _queryExecutor = queryExecutor;
            _dbContext = dbContext;
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        #region execution

        public async Task ExecuteAsync(UpdateCurrentUserAccountCommand command, IExecutionContext executionContext)
        {
            _permissionValidationService.EnforceIsLoggedIn(executionContext.UserContext);
            var userId = executionContext.UserContext.UserId.Value;

            var user = await _dbContext
                .Users
                .FilterCanLogIn()
                .FilterById(userId)
                .SingleOrDefaultAsync();

            EntityNotFoundException.ThrowIfNull(user, userId);

            user.Email = command.Email?.Trim();
            user.FirstName = command.FirstName.Trim();
            user.LastName = command.LastName.Trim();

            await _dbContext.SaveChangesAsync();
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

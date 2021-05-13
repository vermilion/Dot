using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    public class GetUpdateUserCommandByIdQueryHandler
        : IQueryHandler<GetUpdateCommandByIdQuery<UpdateUserCommand>, UpdateUserCommand>
        , ILoggedInPermissionCheckHandler
    {
        private readonly CofoundryDbContext _dbContext;
        private readonly IPermissionValidationService _permissionValidationService;

        public GetUpdateUserCommandByIdQueryHandler(
            CofoundryDbContext dbContext,
            IPermissionValidationService permissionValidationService
            )
        {
            _dbContext = dbContext;
            _permissionValidationService = permissionValidationService;
        }

        public async Task<UpdateUserCommand> ExecuteAsync(GetUpdateCommandByIdQuery<UpdateUserCommand> query, IExecutionContext executionContext)
        {
            var dbUser = await _dbContext
                .Users
                .AsNoTracking()
                .FilterCanLogIn()
                .FilterById(query.Id)
                .SingleOrDefaultAsync();

            if (dbUser == null) return null;

            _permissionValidationService.EnforceCurrentUserOrHasPermission<CofoundryUserReadPermission>(query.Id, executionContext.UserContext);
            var user = new UpdateUserCommand()
            {
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                IsEmailConfirmed = dbUser.IsEmailConfirmed,
                LastName = dbUser.LastName,
                RequirePasswordChange = dbUser.RequirePasswordChange,
                RoleId = dbUser.RoleId,
                UserId = dbUser.RoleId,
                Username = dbUser.Username
            };

            return user;
        }
    }
}

using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Finds a user by a database id returning a UserMicroSummary object if it 
    /// is found, otherwise null.
    /// </summary>
    public class GetUserSummaryByIdQueryHandler
        : IRequestHandler<GetUserSummaryByIdQuery, UserSummary>
    {
        #region constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionValidationService _permissionValidationService;
        private readonly IUserSummaryMapper _userSummaryMapper;

        public GetUserSummaryByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IPermissionValidationService permissionValidationService,
            IUserSummaryMapper userSummaryMapper
            )
        {
            _unitOfWork = unitOfWork;
            _permissionValidationService = permissionValidationService;
            _userSummaryMapper = userSummaryMapper;
        }

        #endregion

        #region execution

        public async Task<UserSummary> ExecuteAsync(GetUserSummaryByIdQuery query, IExecutionContext executionContext)
        {
            var dbResult = await Query(query).SingleOrDefaultAsync();
            var user = _userSummaryMapper.Map(dbResult);

            ValidatePermission(query, executionContext, user);

            return user;
        }

        private IQueryable<User> Query(GetUserSummaryByIdQuery query)
        {
            return _unitOfWork
                .Users()
                .AsNoTracking()
                .Include(u => u.Role)
                .Include(u => u.Creator)
                .Where(u => u.UserId == query.UserId);
        }

        private void ValidatePermission(GetUserSummaryByIdQuery query, IExecutionContext executionContext, UserMicroSummary user)
        {
            if (user == null) return;

            _permissionValidationService.EnforceCurrentUserOrHasPermission<CofoundryUserReadPermission>(query.UserId, executionContext.UserContext);
        }

        #endregion
    }
}

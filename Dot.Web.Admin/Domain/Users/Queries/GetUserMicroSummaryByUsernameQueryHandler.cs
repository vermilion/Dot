using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Finds a user with a specific username address in a specific user area 
    /// returning null if the user could not be found. Note that depending on the
    /// user area, the username may be a copy of the email address.
    /// </summary>
    public class GetUserMicroSummaryByUsernameQueryHandler
        : IRequestHandler<GetUserMicroSummaryByUsernameQuery, UserMicroSummary>
        , IPermissionRestrictedRequestHandler<GetUserMicroSummaryByUsernameQuery>
    {
        #region constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMicroSummaryMapper _userMicroSummaryMapper;

        public GetUserMicroSummaryByUsernameQueryHandler(
            IUnitOfWork unitOfWork,
            IUserMicroSummaryMapper userMicroSummaryMapper
            )
        {
            _unitOfWork = unitOfWork;
            _userMicroSummaryMapper = userMicroSummaryMapper;
        }

        #endregion

        #region execution

        public async Task<UserMicroSummary> ExecuteAsync(GetUserMicroSummaryByUsernameQuery query, IExecutionContext executionContext)
        {
            if (string.IsNullOrWhiteSpace(query.Username)) return null;

            var dbResult = await Query(query).SingleOrDefaultAsync();

            var user = _userMicroSummaryMapper.Map(dbResult);

            return user;
        }

        private IQueryable<User> Query(GetUserMicroSummaryByUsernameQuery query)
        {
            return _unitOfWork
                .Users()
                .AsNoTracking()
                .Where(u => u.Username == query.Username);
        }

        #endregion

        #region permissions

        public IEnumerable<IPermissionApplication> GetPermissions(GetUserMicroSummaryByUsernameQuery query)
        {
            yield return new CofoundryUserReadPermission();
        }

        #endregion
    }
}

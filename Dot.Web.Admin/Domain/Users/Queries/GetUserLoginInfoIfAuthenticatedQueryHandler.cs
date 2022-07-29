using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// A query handler that gets information about a user if the specified credentials
    /// pass an authentication check
    /// </summary>
    public class GetUserLoginInfoIfAuthenticatedQueryHandler 
        : IRequestHandler<GetUserLoginInfoIfAuthenticatedQuery, UserLoginInfo>
    {
        private readonly UserAuthenticationHelper _userAuthenticationHelper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserLoginInfoIfAuthenticatedQueryHandler(
            IUnitOfWork dbContext,
            UserAuthenticationHelper userAuthenticationHelper
            )
        {
            _userAuthenticationHelper = userAuthenticationHelper;
            _unitOfWork = dbContext;
        }

        public async Task<UserLoginInfo> ExecuteAsync(GetUserLoginInfoIfAuthenticatedQuery query, IExecutionContext executionContext)
        {
            if (string.IsNullOrWhiteSpace(query.Username) || string.IsNullOrWhiteSpace(query.Password)) return null;

            var user = await Query(query).FirstOrDefaultAsync();

            return MapResult(query, user);
        }

        private UserLoginInfo MapResult(GetUserLoginInfoIfAuthenticatedQuery query, User user)
        {
            if (_userAuthenticationHelper.IsPasswordCorrect(user, query.Password))
            {
                var result = new UserLoginInfo
                {
                    RequirePasswordChange = user.RequirePasswordChange,
                    UserId = user.UserId
                };

                return result;
            }

            return null;
        }

        private IQueryable<User> Query(GetUserLoginInfoIfAuthenticatedQuery query)
        {
            return _unitOfWork
                .Users()
                .AsNoTracking()
                .FilterCanLogIn()
                .Where(u => u.Username == query.Username);
        }
    }
}

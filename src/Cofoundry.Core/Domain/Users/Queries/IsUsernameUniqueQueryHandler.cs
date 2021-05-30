using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Determines if a username is unique within a specific UserArea.
    /// Usernames only have to be unique per UserArea.
    /// </summary>
    public class IsUsernameUniqueQueryHandler 
        : IRequestHandler<IsUsernameUniqueQuery, bool>
        , IPermissionRestrictedRequestHandler<IsUsernameUniqueQuery>
    {
        #region constructor

        private readonly DbContextCore _dbContext;

        public IsUsernameUniqueQueryHandler(
            DbContextCore dbContext
            )
        {
            _dbContext = dbContext;
        }
        
        #endregion

        #region execution

        public async Task<bool> ExecuteAsync(IsUsernameUniqueQuery query, IExecutionContext executionContext)
        {
            var exists = await Exists(query).AnyAsync();
            return !exists;
        }

        private IQueryable<User> Exists(IsUsernameUniqueQuery query)
        {
            return _dbContext
                .Users
                .AsNoTracking()
                .FilterActive()
                .Where(u => u.UserId != query.UserId && u.Username == query.Username);
        }

        #endregion

        #region permissions

        public IEnumerable<IPermissionApplication> GetPermissions(IsUsernameUniqueQuery query)
        {
            yield return new CofoundryUserReadPermission();
        }

        #endregion
    }

}

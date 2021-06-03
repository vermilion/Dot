using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Seaches roles based on simple filter criteria and returns a paged result. 
    /// </summary>
    public class SearchRolesQueryHandler 
        : IRequestHandler<SearchRolesQuery, PagedQueryResult<RoleMicroSummary>>
        , IPermissionRestrictedRequestHandler<SearchRolesQuery>
    {
        #region constructor

        private readonly DbContextCore _dbContext;
        private readonly IRoleMicroSummaryMapper _roleMicroSummaryMapper;

        public SearchRolesQueryHandler(
            DbContextCore dbContext,
            IMediator queryExecutor,
            IRoleMicroSummaryMapper roleMicroSummaryMapper
            )
        {
            _dbContext = dbContext;
            _roleMicroSummaryMapper = roleMicroSummaryMapper;
        }

        #endregion

        #region execution

        public async Task<PagedQueryResult<RoleMicroSummary>> ExecuteAsync(SearchRolesQuery query, IExecutionContext executionContext)
        {
            var dbPagedResult = await CreateQuery(query).ToPagedResultAsync(query);

            var mappedResults = dbPagedResult
                .Items
                .Select(_roleMicroSummaryMapper.Map);

            return dbPagedResult.ChangeType(mappedResults);
        }

        #endregion

        #region helpers

        private IQueryable<Role> CreateQuery(SearchRolesQuery query)
        {
            var dbQuery = _dbContext
                .Roles
                .AsNoTracking()
                .AsQueryable();

            if (query.ExcludeAnonymous)
            {
                dbQuery = dbQuery
                    .Where(r => r.RoleCode != AnonymousRole.AnonymousRoleCode);
            }

            if (!string.IsNullOrEmpty(query.Text))
            {
                var text = query.Text.Trim();
                dbQuery = dbQuery.Where(r => r.Title.Contains(text))
                    .OrderByDescending(r => r.Title.StartsWith(text))
                    .ThenByDescending(r => r.Title);
            }
            else
            {
                dbQuery = dbQuery
                    .OrderBy(r => r.Title);
            }

            return dbQuery;
        }

        #endregion

        #region permissions

        public IEnumerable<IPermissionApplication> GetPermissions(SearchRolesQuery command)
        {
            yield return new RoleReadPermission();
        }

        #endregion
    }
}

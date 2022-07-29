using Cofoundry.Domain;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.Internal;
using Dot.EFCore.UnitOfWork;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cofoundry.Web.Admin
{
    public class GetAllEndpoint : Endpoint<GetAllUsersRequestModel>
    {
        public IUserContextService UserContextService { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public IUserSummaryMapper UserSummaryMapper { get; set; }

        public override void Configure()
        {
            Post("/api/Users/GetAll");
        }

        public override async Task HandleAsync(GetAllUsersRequestModel req, CancellationToken ct)
        {
            var dbPagedResult = await CreateQuery(req).ToPagedResultAsync(req);
            var mappedResult = dbPagedResult
                .Items
                .Select(UserSummaryMapper.Map);

            var result = dbPagedResult.ChangeType(mappedResult);

            await SendAsync(result, cancellation: ct);
        }

        private IQueryable<User> CreateQuery(GetAllUsersRequestModel query)
        {
            var dbQuery = UnitOfWork.Users()
                .AsNoTracking()
                .Include(u => u.Role)
                .FilterCanLogIn();

            if (!string.IsNullOrEmpty(query.Email))
            {
                dbQuery = dbQuery.Where(u => u.Email.Contains(query.Email));
            }

            // Filter by name
            if (!string.IsNullOrEmpty(query.Name))
            {
                var names = query.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string name in names)
                {
                    // See http://stackoverflow.com/a/7288269/486434 for why this is copied into a new variable
                    string localName = name;

                    dbQuery = dbQuery.Where(u => u.FirstName.Contains(localName) || u.LastName.Contains(localName));
                }

                // Order by exact matches first
                dbQuery = dbQuery
                    .OrderByDescending(u => names.Contains(u.FirstName) && names.Contains(u.LastName))
                    .ThenByDescending(u => names.Contains(u.FirstName) || names.Contains(u.LastName));
            }
            else
            {
                dbQuery = dbQuery.OrderBy(u => u.LastName);
            }

            return dbQuery;
        }
    }

    /// <summary>
    /// Searches users based on simple filter criteria and returns a paged result. 
    /// </summary>
    public class GetAllUsersRequestModel : SimplePageableQuery
    {
        /// <summary>
        /// Filter by first or last name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Filter by email address
        /// </summary>
        public string Email { get; set; }
    }
}
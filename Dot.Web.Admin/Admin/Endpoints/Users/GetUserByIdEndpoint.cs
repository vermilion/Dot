using Cofoundry.Domain.Data;
using Cofoundry.Domain.Internal;
using Dot.EFCore.UnitOfWork;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cofoundry.Web.Admin
{
    public class GetUserByIdEndpoint : Endpoint<GetUserByIdRequestModel>
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IUserDetailsMapper UserDetailsMapper { get; set; }

        public override void Configure()
        {
            Post("/api/Users/GetById");
        }

        public override async Task HandleAsync(GetUserByIdRequestModel req, CancellationToken ct)
        {
            var dbUser = await UnitOfWork.Users()
                .AsNoTracking()
                .WithSpecification(new UserByIdSpec(req.UserId))
                .Include(u => u.Creator)
                .Include(u => u.Role)
                .SingleOrDefaultAsync();

            var user = UserDetailsMapper.Map(dbUser);

            if (user != null)
            {
                //_permissionValidationService.EnforceCurrentUserOrHasPermission<CofoundryUserReadPermission>(query.UserId, executionContext.UserContext);
            }

            await SendAsync(user, cancellation: ct);
        }
    }

    public class GetUserByIdRequestModel
    {
        /// <summary>
        /// Database id of the user.
        /// </summary>
        public int UserId { get; set; }
    }
}
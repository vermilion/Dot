using Cofoundry.Core;
using Cofoundry.Domain;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.Internal;
using Dot.EFCore.UnitOfWork;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cofoundry.Web.Admin
{
    public class GetCurrentUserEndpoint : Endpoint<object>
    {
        public IUserContextService UserContextService { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public IUserAccountDetailsMapper UserAccountDetailsMapper { get; set; }

        public override void Configure()
        {
            Post("/api/Account/CurrentUser/Get");
        }

        public override async Task HandleAsync(object req, CancellationToken ct)
        {
            var userContext = await UserContextService.GetCurrentContextAsync();
            
            var dbResult = await UnitOfWork.Users()
                .AsNoTracking()
                .WithSpecification(new UserByIdSpec(userContext.UserId.Value))
                .Include(u => u.Creator)
                .SingleOrDefaultAsync(ct);

            EntityNotFoundException.ThrowIfNull(dbResult, userContext.UserId);

            var user = UserAccountDetailsMapper.Map(dbResult);

            await SendOkAsync(ct);
        }
    }
}
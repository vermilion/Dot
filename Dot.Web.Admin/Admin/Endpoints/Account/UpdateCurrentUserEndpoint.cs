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
    public class UpdateCurrentUserEndpoint : Endpoint<UpdateCurrentUserAccountCommand>
    {
        public IUserContextService UserContextService { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public override void Configure()
        {
            Post("/api/Account/CurrentUser/UpdateAccount");
        }

        public override async Task HandleAsync(UpdateCurrentUserAccountCommand req, CancellationToken ct)
        {
            var userContext = await UserContextService.GetCurrentContextAsync();

            var user = await UnitOfWork.Users()
                .WithSpecification(new UserByIdSpec(userContext.UserId))
                .SingleOrDefaultAsync(ct);

            EntityNotFoundException.ThrowIfNull(user, userContext.UserId);

            user.Email = req.Email?.Trim();
            user.FirstName = req.FirstName.Trim();
            user.LastName = req.LastName.Trim();

            await UnitOfWork.SaveChangesAsync(ct);

            await SendOkAsync(ct);
        }
    }
}
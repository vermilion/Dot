using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;
using System.Linq;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.GetAll
{
    public class GetAllHandler : EntitySelectPagedHandlerBase<RoleClaim, RoleClaimModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override IQueryable<RoleClaim> BuildQuery(GetAllRequest request, IQueryable<RoleClaim> query)
        {
            return query.Where(x => Equals(x.RoleId, request.RoleId));
        }
    }
}

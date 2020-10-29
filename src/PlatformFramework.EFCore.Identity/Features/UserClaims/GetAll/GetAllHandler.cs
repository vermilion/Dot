using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;
using System.Linq;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.GetAll
{
    public class GetAllHandler : EntitySelectPagedHandlerBase<UserClaim, UserClaimModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override IQueryable<UserClaim> BuildQuery(GetAllRequest request, IQueryable<UserClaim> query)
        {
            return query.Where(x => Equals(x.UserId, request.UserId));
        }
    }
}

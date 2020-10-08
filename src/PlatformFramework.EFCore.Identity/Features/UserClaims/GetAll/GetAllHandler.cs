using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.GetAll
{
    public class GetAllHandler : EntitySelectPagedHandlerBase<UserClaim, UserClaimModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.Get
{
    public class GetHandler : EntitySelectSingleHandlerBase<UserClaim, UserClaimModel, GetRequest>
    {
        public GetHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

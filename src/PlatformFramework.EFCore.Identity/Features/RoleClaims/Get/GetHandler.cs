using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Get
{
    public class GetHandler : EntitySelectSingleHandlerBase<RoleClaim, RoleClaimModel, GetRequest>
    {
        public GetHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

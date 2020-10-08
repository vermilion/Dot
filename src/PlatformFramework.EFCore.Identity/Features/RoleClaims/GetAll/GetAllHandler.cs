using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.GetAll
{
    public class GetAllHandler : EntitySelectPagedHandlerBase<RoleClaim, RoleClaimModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

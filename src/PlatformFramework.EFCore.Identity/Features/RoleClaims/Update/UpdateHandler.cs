using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Update
{
    public class UpdateHandler : EntityUpdateHandlerBase<RoleClaim, RoleClaimModel, UpdateRequest>
    {
        public UpdateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

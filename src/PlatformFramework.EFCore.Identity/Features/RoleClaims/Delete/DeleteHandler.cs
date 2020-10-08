using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Delete
{
    public class DeleteHandler : EntityDeleteHandlerBase<RoleClaim, RoleClaimModel, DeleteRequest>
    {
        public DeleteHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

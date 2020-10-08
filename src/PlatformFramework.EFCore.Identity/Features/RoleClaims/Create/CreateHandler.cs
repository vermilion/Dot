using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Create
{
    public class CreateHandler : EntityCreateHandlerBase<RoleClaim, RoleClaimModel, CreateRequest>
    {
        public CreateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

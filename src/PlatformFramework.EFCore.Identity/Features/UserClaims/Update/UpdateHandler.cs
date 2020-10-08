using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.Update
{
    public class UpdateHandler : EntityUpdateHandlerBase<UserClaim, UserClaimModel, UpdateRequest>
    {
        public UpdateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

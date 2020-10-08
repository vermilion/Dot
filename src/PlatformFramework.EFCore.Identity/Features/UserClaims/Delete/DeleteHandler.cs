using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.Delete
{
    public class DeleteHandler : EntityDeleteHandlerBase<UserClaim, UserClaimModel, DeleteRequest>
    {
        public DeleteHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

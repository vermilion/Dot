using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.Create
{
    public class CreateHandler : EntityCreateHandlerBase<UserClaim, UserClaimModel, CreateRequest>
    {
        public CreateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

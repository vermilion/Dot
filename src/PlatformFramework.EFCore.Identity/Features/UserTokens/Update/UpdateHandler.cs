using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.Update
{
    public class UpdateHandler : EntityUpdateHandlerBase<UserToken, UserTokenModel, UpdateRequest>
    {
        public UpdateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

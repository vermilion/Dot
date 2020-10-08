using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.Update
{
    public class UpdateHandler : EntityUpdateHandlerBase<UserLogin, UserLoginModel, UpdateRequest>
    {
        public UpdateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

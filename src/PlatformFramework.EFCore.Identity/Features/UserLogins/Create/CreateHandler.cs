using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.Create
{
    public class CreateHandler : EntityCreateHandlerBase<UserLogin, UserLoginModel, CreateRequest>
    {
        public CreateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

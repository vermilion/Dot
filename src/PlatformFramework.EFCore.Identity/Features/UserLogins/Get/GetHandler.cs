using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.Get
{
    public class GetHandler : EntitySelectSingleHandlerBase<UserLogin, UserLoginModel, GetRequest>
    {
        public GetHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

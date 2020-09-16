using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.Users.Get
{
    public class GetHandler : EntitySelectSingleHandlerBase<User, UserModel, GetRequest>
    {
        public GetHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

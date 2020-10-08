using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.Create
{
    public class CreateHandler : EntityCreateHandlerBase<UserToken, UserTokenModel, CreateRequest>
    {
        public CreateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

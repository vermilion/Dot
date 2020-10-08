using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.Get
{
    public class GetHandler : EntitySelectSingleHandlerBase<UserToken, UserTokenModel, GetRequest>
    {
        public GetHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

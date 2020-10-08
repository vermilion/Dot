using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.GetAll
{
    public class GetAllHandler : EntitySelectPagedHandlerBase<UserToken, UserTokenModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

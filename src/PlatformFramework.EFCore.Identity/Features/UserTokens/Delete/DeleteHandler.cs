using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.Delete
{
    public class DeleteHandler : EntityDeleteHandlerBase<UserToken, UserTokenModel, DeleteRequest>
    {
        public DeleteHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

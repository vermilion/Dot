using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.Delete
{
    public class DeleteHandler : EntityDeleteHandlerBase<UserLogin, UserLoginModel, DeleteRequest>
    {
        public DeleteHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

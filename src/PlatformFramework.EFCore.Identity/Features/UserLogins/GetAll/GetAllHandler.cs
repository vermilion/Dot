using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.GetAll
{
    public class GetAllHandler : EntitySelectPagedHandlerBase<UserLogin, UserLoginModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}

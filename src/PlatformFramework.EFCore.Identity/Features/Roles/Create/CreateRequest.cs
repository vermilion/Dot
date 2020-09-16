using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;
using System.Linq;

namespace PlatformFramework.EFCore.Identity.Features.Roles.Create
{
    public class CreateRequest : EntityCreateRequest<RoleModel>
    {
        public CreateRequest(RoleModel model) : base(model)
        {
        }
    }
}

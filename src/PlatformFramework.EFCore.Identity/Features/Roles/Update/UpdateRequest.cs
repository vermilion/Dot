using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.Roles.Update
{
    public class UpdateRequest : EntityUpdateRequest<RoleModel>
    {
        public UpdateRequest(int id, RoleModel model) : base(id, model)
        {
        }
    }
}

using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Features.Roles.Delete
{
    public class DeleteRequest : EntityDeleteRequest<RoleModel>
    {
        public DeleteRequest(int id)
            : base(id)
        {
        }
    }
}

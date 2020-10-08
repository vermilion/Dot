using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Delete
{
    public class DeleteRequest : EntityDeleteRequest<RoleClaimModel>
    {
        public DeleteRequest(int id)
            : base(id)
        {
        }
    }
}

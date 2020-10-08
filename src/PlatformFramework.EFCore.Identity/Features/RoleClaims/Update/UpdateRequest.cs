using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Update
{
    public class UpdateRequest : EntityUpdateRequest<RoleClaimModel>
    {
        public UpdateRequest(int id, RoleClaimModel model) : base(id, model)
        {
        }
    }
}

using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Create
{
    public class CreateRequest : EntityCreateRequest<RoleClaimModel>
    {
        public CreateRequest(RoleClaimModel model) : base(model)
        {
        }
    }
}

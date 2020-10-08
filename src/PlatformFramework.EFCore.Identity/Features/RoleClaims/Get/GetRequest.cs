using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Get
{
    public class GetRequest : EntitySelectSingleRequest<RoleClaimModel>
    {
        public GetRequest(int id) : base(id)
        {
        }
    }
}

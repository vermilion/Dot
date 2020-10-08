using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.Get
{
    public class GetRequest : EntitySelectSingleRequest<UserClaimModel>
    {
        public GetRequest(int id) : base(id)
        {
        }
    }
}

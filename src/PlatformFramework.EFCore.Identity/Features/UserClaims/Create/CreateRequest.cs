using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.Create
{
    public class CreateRequest : EntityCreateRequest<UserClaimModel>
    {
        public CreateRequest(UserClaimModel model) : base(model)
        {
        }
    }
}

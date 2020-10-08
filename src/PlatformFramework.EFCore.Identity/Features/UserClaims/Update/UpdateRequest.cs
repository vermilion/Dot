using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.Update
{
    public class UpdateRequest : EntityUpdateRequest<UserClaimModel>
    {
        public UpdateRequest(int id, UserClaimModel model) : base(id, model)
        {
        }
    }
}

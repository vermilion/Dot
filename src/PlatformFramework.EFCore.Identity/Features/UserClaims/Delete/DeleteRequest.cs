using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.Delete
{
    public class DeleteRequest : EntityDeleteRequest<UserClaimModel>
    {
        public DeleteRequest(int id)
            : base(id)
        {
        }
    }
}

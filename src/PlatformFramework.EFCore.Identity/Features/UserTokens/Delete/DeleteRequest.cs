using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.Delete
{
    public class DeleteRequest : EntityDeleteRequest<UserTokenModel>
    {
        public DeleteRequest(int id)
            : base(id)
        {
        }
    }
}

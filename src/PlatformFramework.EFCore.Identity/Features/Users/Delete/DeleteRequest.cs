using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Users.Delete
{
    public class DeleteRequest : EntityDeleteRequest<UserModel>
    {
        public DeleteRequest(int id)
            : base(id)
        {
        }
    }
}

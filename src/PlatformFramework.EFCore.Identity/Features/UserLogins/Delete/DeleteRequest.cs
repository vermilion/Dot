using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.Delete
{
    public class DeleteRequest : EntityDeleteRequest<UserLoginModel>
    {
        public DeleteRequest(int id)
            : base(id)
        {
        }
    }
}

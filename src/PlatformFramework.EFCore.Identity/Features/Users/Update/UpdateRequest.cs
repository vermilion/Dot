using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Users.Update
{
    public class UpdateRequest : EntityUpdateRequest<UserModel>
    {
        public UpdateRequest(int id, UserModel model) : base(id, model)
        {
        }
    }
}

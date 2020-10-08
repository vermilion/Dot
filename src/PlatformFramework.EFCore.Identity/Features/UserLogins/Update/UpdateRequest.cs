using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.Update
{
    public class UpdateRequest : EntityUpdateRequest<UserLoginModel>
    {
        public UpdateRequest(int id, UserLoginModel model) : base(id, model)
        {
        }
    }
}

using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Users.Get
{
    public class GetRequest : EntitySelectSingleRequest<UserModel>
    {
        public GetRequest(int id) : base(id)
        {
        }
    }
}

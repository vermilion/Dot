using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Roles.Get
{
    public class GetRequest : EntitySelectSingleRequest<RoleModel>
    {
        public GetRequest(int id) : base(id)
        {
        }
    }
}

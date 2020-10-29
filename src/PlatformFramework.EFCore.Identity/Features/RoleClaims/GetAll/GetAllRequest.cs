using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.GetAll
{
    public class GetAllRequest : EntityPagedSelectRequest<RoleClaimModel>
    {
        public GetAllRequest(int roleId, PagedModel model)
            : base(model.Limit, model.Offset)
        {
            RoleId = roleId;
        }

        public int RoleId { get; }
    }
}

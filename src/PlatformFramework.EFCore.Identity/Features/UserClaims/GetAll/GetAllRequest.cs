using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserClaims.GetAll
{
    public class GetAllRequest : EntityPagedSelectRequest<UserClaimModel>
    {
        public GetAllRequest(PagedModel model)
            : base(model.Limit, model.Offset)
        {
        }
    }
}

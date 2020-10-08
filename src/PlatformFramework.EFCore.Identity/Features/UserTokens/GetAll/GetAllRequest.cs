using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.GetAll
{
    public class GetAllRequest : EntityPagedSelectRequest<UserTokenModel>
    {
        public GetAllRequest(PagedModel model)
            : base(model.Limit, model.Offset)
        {
        }
    }
}

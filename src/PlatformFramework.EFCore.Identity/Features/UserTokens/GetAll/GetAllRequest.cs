using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.GetAll
{
    public class GetAllRequest : EntityPagedSelectRequest<UserTokenModel>
    {
        public GetAllRequest(int userId, PagedModel model)
            : base(model.Limit, model.Offset)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}

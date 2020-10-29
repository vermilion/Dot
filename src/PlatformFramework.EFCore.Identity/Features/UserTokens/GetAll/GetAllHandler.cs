using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;
using System.Linq;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.GetAll
{
    public class GetAllHandler : EntitySelectPagedHandlerBase<UserToken, UserTokenModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override IQueryable<UserToken> BuildQuery(GetAllRequest request, IQueryable<UserToken> query)
        {
            return query.Where(x => Equals(x.UserId, request.UserId));
        }
    }
}

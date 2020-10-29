using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;
using System.Linq;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.GetAll
{
    public class GetAllHandler : EntitySelectPagedHandlerBase<UserLogin, UserLoginModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override IQueryable<UserLogin> BuildQuery(GetAllRequest request, IQueryable<UserLogin> query)
        {
            return query.Where(x => Equals(x.UserId, request.UserId));
        }
    }
}

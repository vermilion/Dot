using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Identity.Context;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Stores
{
    public class UserStore<TDbContext> : UserStore<User, Role, DbContext, long>
        where TDbContext : IdentityDbContextCore
    {
        public UserStore(TDbContext context, IdentityErrorDescriber? describer = null)
            : base(context, describer)
        {
        }
    }
}

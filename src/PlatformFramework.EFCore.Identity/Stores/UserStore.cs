using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Stores
{
    public class UserStore : UserStore<User, Role, DbContext, long>
    {
        public UserStore(DbContext context, IdentityErrorDescriber? describer = null)
            : base(context, describer)
        {
        }
    }
}

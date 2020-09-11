using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Stores
{

    public class RoleStore : RoleStore<Role, DbContext, long>
    {
        public RoleStore(DbContext context, IdentityErrorDescriber? describer = null)
            : base(context, describer)
        {
        }
    }
}

using Microsoft.AspNetCore.Identity;
using PlatformFramework.EFCore.Abstractions;
using System.Collections.Generic;

namespace PlatformFramework.EFCore.Identity.Entities
{
    public class Role : IdentityRole<int>, IEntity
    {
        public ICollection<RoleClaim> Claims { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using PlatformFramework.EFCore.Abstractions;
using System.Collections.Generic;

namespace PlatformFramework.EFCore.Identity.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public ICollection<UserToken> Tokens { get; set; }
        public ICollection<UserLogin> Logins { get; set; }
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserRole> Roles { get; set; }
    }
}

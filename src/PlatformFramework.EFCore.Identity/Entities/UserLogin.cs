using Microsoft.AspNetCore.Identity;
using PlatformFramework.EFCore.Abstractions;

namespace PlatformFramework.EFCore.Identity.Entities
{
    public class UserLogin : IdentityUserLogin<int>, IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for this user login
        /// </summary>
        public int Id { get; set; }
    }
}

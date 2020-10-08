using Microsoft.AspNetCore.Identity;
using PlatformFramework.EFCore.Abstractions;

namespace PlatformFramework.EFCore.Identity.Entities
{
    public class UserToken : IdentityUserToken<int>, IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for this user token
        /// </summary>
        public int Id { get; set; }
    }
}

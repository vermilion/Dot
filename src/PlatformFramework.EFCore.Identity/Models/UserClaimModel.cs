using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Models
{
    public class UserClaimModel : ReadModel
    {
        /// <summary>
        /// Gets or sets the primary key of the user that the claim belongs to
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// Gets or sets the claim type for this claim
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the claim value for this claim
        /// </summary>
        public virtual string ClaimValue { get; set; }
    }
}

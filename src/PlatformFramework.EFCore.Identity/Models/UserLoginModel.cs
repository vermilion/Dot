using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Models
{
    public class UserLoginModel : ReadModel
    {
        /// <summary>
        /// Gets or sets the primary key of the user that the login belongs to
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// Gets or sets the login provider for the login (e.g. facebook, google)
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the friendly name used in a UI for this login
        /// </summary>
        public virtual string ProviderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the unique provider identifier for this login
        /// </summary>
        public virtual string ProviderKey { get; set; }
    }
}

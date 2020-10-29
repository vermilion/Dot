using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Models
{
    public class UserTokenModel : ReadModel
    {
        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// Gets or sets the LoginProvider this token is from
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the name of the token
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the token value
        /// </summary>
        public virtual string Value { get; set; }
    }
}

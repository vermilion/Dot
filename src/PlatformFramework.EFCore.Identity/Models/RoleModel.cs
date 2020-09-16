using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Models
{
    public class RoleModel : ReadModel
    {
        /// <summary>
        /// Gets or sets the name for this role
        /// </summary>
        public string Name { get; set; }
    }
}

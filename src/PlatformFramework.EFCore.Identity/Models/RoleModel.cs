using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Models
{
    public class RoleModel : ReadModel
    {
        //
        // Summary:
        //     A random value that should change whenever a role is persisted to the store
        public string ConcurrencyStamp { get; set; }

        //
        // Summary:
        //     Gets or sets the name for this role.
        public string Name { get; set; }

        //
        // Summary:
        //     Gets or sets the normalized name for this role.
        public string NormalizedName { get; set; }
    }
}

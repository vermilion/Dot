using System.Security.Claims;

namespace PlatformFramework.Web.Runtime
{
    public static class UserClaimTypes
    {
        public const string UserName = ClaimTypes.Name;
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string Role = ClaimTypes.Role;
        public const string DisplayName = nameof(DisplayName);
        public const string Permission = nameof(Permission);
        public const string PackedPermission = nameof(PackedPermission);
        public const string ImpersonatorUserId = nameof(ImpersonatorUserId);
    }
}
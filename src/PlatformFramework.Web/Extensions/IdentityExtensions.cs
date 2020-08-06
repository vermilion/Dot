using System;
using System.Security.Claims;
using System.Security.Principal;
using PlatformFramework.Shared.Extensions;
using PlatformFramework.Web.Runtime;

namespace PlatformFramework.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static T FindUserId<T>(this IIdentity identity) where T : IEquatable<T>
        {
            return identity.FindUserId().FromString<T>();
        }

        public static string FindUserId(this IIdentity identity)
        {
            var value = identity.FindUserClaimValue(UserClaimTypes.UserId);
            return value;
        }

        public static string FindImpersonatorUserId(this IIdentity identity)
        {
            return identity.FindUserClaimValue(UserClaimTypes.ImpersonatorUserId);
        }

        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return identity.FindFirst(claimType)?.Value;
        }

        public static string FindUserClaimValue(this IIdentity identity, string claimType)
        {
            return (identity as ClaimsIdentity)?.FindFirstValue(claimType);
        }

        public static string FindUserDisplayName(this IIdentity identity)
        {
            var displayName = identity.FindUserClaimValue(UserClaimTypes.UserName);
            return string.IsNullOrWhiteSpace(displayName) ? FindUserName(identity) : displayName;
        }

        public static string FindUserName(this IIdentity identity)
        {
            return identity.FindUserClaimValue(UserClaimTypes.UserName);
        }
    }
}
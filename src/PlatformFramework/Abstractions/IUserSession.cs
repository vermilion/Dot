using System.Collections.Generic;
using System.Security.Claims;

namespace PlatformFramework.Abstractions
{
    /// <summary>
    ///     Defines some session information that can be useful for applications.
    /// </summary>
    public interface IUserSession
    {
        bool IsAuthenticated { get; }

        /// <summary>
        ///     Gets current UserId or null.
        ///     It can be null if no user logged in.
        /// </summary>
        string? UserId { get; }

        /// <summary>
        ///     Gets current UserName or null.
        ///     It can be null if no user logged in.
        /// </summary>
        string? UserName { get; }

        /// <summary>
        ///     Gets current user's Roles
        ///     It can be null if no user logged in.
        /// </summary>
        IReadOnlyList<string>? Roles { get; }

        /// <summary>
        ///     Gets current user's Claims
        ///     It can be null if no user logged in.
        /// </summary>
        IReadOnlyList<Claim>? Claims { get; }
    }
}
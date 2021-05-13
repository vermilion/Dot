using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Cofoundry.Core;
using Cofoundry.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Cofoundry.Web
{
    /// <summary>
    /// Service to abstract away the management of a users current browser session and 
    /// authentication cookie.
    /// </summary>
    public class UserSessionService : IUserSessionService
    {
        /// <summary>
        /// SignInUser doesn't always update the HttpContext.Current.User so we set this
        /// cache value instead which will last for the lifetime of the request. This is only used
        /// when signing in and isn't otherwise cached.
        /// </summary>
        private int? _userIdCache = null;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSessionService(
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the UserId of the user authenticated for the
        /// current request under the ambient authentication scheme.
        /// </summary>
        /// <returns>
        /// Integer UserId or null if the user is not logged in for the ambient
        /// authentication scheme.
        /// </returns>
        public int? GetCurrentUserId()
        {
            if (_userIdCache.HasValue) return _userIdCache;

            var user = _httpContextAccessor?.HttpContext?.User;
            var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null) return null;

            // Otherwise get it from the Identity
            var userId = IntParser.ParseOrNull(userIdClaim.Value);
            return userId;
        }

        /// <summary>
        /// Logs the specified UserId into the current session.
        /// </summary>
        /// <param name="userAreaCode">Unique code of the user area to log the user into (required).</param>
        /// <param name="userId">UserId belonging to the owner of the current session.</param>
        /// <param name="rememberUser">
        /// True if the session should last indefinately; false if the 
        /// session should close after a timeout period.
        /// </param>
        public Task LogUserInAsync(int userId, bool rememberUser)
        {
            if (userId < 1) throw new ArgumentOutOfRangeException(nameof(userId));

            var stringId = Convert.ToString(userId);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, stringId),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(claimsIdentity);

            _userIdCache = userId;

            AuthenticationProperties authProperties = null;
            if (rememberUser)
            {
                authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
            }

            return _httpContextAccessor.HttpContext.SignInAsync(userPrincipal, authProperties);
        }

        /// <summary>
        /// Logs the user out of the specified user area.
        /// </summary>
        /// <param name="userAreaCode">Unique code of the user area to log the user out of (required).</param>
        public Task LogUserOutAsync()
        {
            ClearCache();

            return _httpContextAccessor.HttpContext.SignOutAsync();
        }

        private void ClearCache()
        {
            _userIdCache = null;
        }
    }
}

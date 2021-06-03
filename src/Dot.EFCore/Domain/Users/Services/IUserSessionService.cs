using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Service to abstract away the management of a users current browser session and 
    /// authentication cookie.
    /// </summary>
    public interface IUserSessionService
    {
        /// <summary>
        /// Gets the UserId of the user currently logged
        /// in to this session
        /// </summary>
        /// <returns>
        /// UserId of the user currently logged
        /// in to this session
        /// </returns>
        int? GetCurrentUserId();

        /// <summary>
        /// Logs the specified UserId into the current session.
        /// </summary>
        /// <param name="userId">UserId belonging to the owner of the current session.</param>
        /// <param name="rememberUser">
        /// True if the session should last indefinately; false if the 
        /// session should close after a timeout period.
        /// </param>
        Task LogUserInAsync(int userId, bool rememberUser);

        /// <summary>
        /// Logs the user out of the specified user area.
        /// </summary>
        /// <param name="userAreaCode">Unique code of the user area to log the user out of (required).</param>
        Task LogUserOutAsync();
    }
}

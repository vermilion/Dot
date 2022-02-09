﻿using System.Threading.Tasks;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Service for retreiving user connection information.
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Get the connection context of the current user. By default the UserContext
        /// is cached for the lifetime of the service (per request in web scenarios).
        /// </summary>
        Task<IUserContext> GetCurrentContextAsync();

        /// <summary>
        /// Use this to get a user context for the system user, useful
        /// if you need to impersonate the user to perform an action with elevated 
        /// privileges.
        /// </summary>
        Task<IUserContext> GetSystemUserContextAsync();

        /// <summary>
        /// Clears out the cached user context if one exists. Typically the user 
        /// context is cached for the duration of the request so it needs clearing if
        /// it changes (i.e. logged in or out).
        /// </summary>
        void ClearCache();
    }
}

using Cofoundry.Core;
using Microsoft.AspNetCore.Http;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Service for retreiving connection information about a client connected 
    /// to the application e.g. IPAddress and the UserAgent string.
    /// </summary>
    public class ClientConnectionService : IClientConnectionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientConnectionService(
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets an object that represents the current client
        /// connected to the application in the current request.
        /// </summary>
        public ClientConnectionInfo GetConnectionInfo()
        {
            var context = _httpContextAccessor.HttpContext;

            var info = new ClientConnectionInfo();

            if (context?.Request != null)
            {
                info.IPAddress = context?.Connection?.RemoteIpAddress?.ToString();
                if (context?.Request?.Headers != null)
                {
                    if (context.Request.Headers.TryGetValue("User-Agent", out var value) == true)
                    {
                        info.UserAgent = value;
                    }
                }
            }

            return info;
        }
    }

}

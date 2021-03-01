using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Identity.Abstrations;
using PlatformFramework.Eventing;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class LogoutRequestHandler : RequestHandler<LogoutRequest, LogoutResponse>
    {
        private readonly IJwtAuthService _jwtAuthManager;
        private readonly ILogger<LoginRequestHandler> _logger;

        public LogoutRequestHandler(
             IJwtAuthService jwtAuthService,
             ILogger<LoginRequestHandler> logger)
        {
            _jwtAuthManager = jwtAuthService;
            _logger = logger;
        }

        public override async Task<LogoutResponse> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            await _jwtAuthManager.RemoveRefreshTokenByUserName(request.Name); // can be more specific to ip, user agent, device name, etc.
            _logger.LogInformation($"User [{request.Name}] logged out the system.");

            return new LogoutResponse.Success();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Identity.Abstrations;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IClockProvider _clockProvider;
        private readonly IJwtAuthService _jwtAuthManager;
        private readonly ILogger<LoginRequestHandler> _logger;

        public RefreshTokenRequestHandler(
            UserManager<User> userManager,
            IClockProvider clockProvider,
            IJwtAuthService jwtAuthService,
            ILogger<LoginRequestHandler> logger)
        {
            _userManager = userManager;
            _clockProvider = clockProvider;
            _jwtAuthManager = jwtAuthService;
            _logger = logger;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("User is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return new RefreshTokenResponse.Unauthorized();
                }

                var userName = await _jwtAuthManager
                    .TryGetUserWithToken(request.RefreshToken, _clockProvider.Now)
                    .ConfigureAwait(false);

                var appUser = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
                var claims = await _userManager.GetClaimsAsync(appUser).ConfigureAwait(false);
                var roles = await _userManager.GetRolesAsync(appUser).ConfigureAwait(false);

                claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, appUser.UserName));

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtResult = await _jwtAuthManager.GenerateTokens(userName, claims, _clockProvider.Now).ConfigureAwait(false);
                _logger.LogInformation("User has refreshed JWT token");

                return new RefreshTokenResponse.Success(new TokenResponse
                {
                    AccessToken = jwtResult.AccessToken,
                }, jwtResult.RefreshToken.TokenString);
            }
            catch (SecurityTokenException e)
            {
                // return 401 so that the client side can redirect the user to login page
                return new RefreshTokenResponse.Unauthorized(e.Message);
            }
        }
    }
}

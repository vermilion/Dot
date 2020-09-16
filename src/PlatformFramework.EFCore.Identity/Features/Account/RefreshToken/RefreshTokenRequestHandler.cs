using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PlatformFramework.EFCore.Identity.Abstrations;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtAuthService _jwtAuthManager;
        private readonly ILogger<LoginRequestHandler> _logger;

        public RefreshTokenRequestHandler(
             UserManager<User> userManager,
             SignInManager<User> signInManager,
             IJwtAuthService jwtAuthService,
             ILogger<LoginRequestHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthManager = jwtAuthService;
            _logger = logger;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"User [{request.UserName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return new RefreshTokenResponse.Unauthorized();
                }

                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, request.AccessToken!, DateTime.Now);
                _logger.LogInformation($"User [{request.UserName}] has refreshed JWT token.");

                return new RefreshTokenResponse.Success(new TokenResponse
                {
                    UserName = request.UserName,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                // return 401 so that the client side can redirect the user to login page
                return new RefreshTokenResponse.Unauthorized(e.Message);
            }
        }
    }
}

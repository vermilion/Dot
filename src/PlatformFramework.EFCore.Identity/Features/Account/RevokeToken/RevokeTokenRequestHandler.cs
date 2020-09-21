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
    public class RevokeTokenRequestHandler : IRequestHandler<RevokeTokenRequest, RevokeTokenResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtAuthService _jwtAuthManager;
        private readonly ILogger<LoginRequestHandler> _logger;

        public RevokeTokenRequestHandler(
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

        public async Task<RevokeTokenResponse> Handle(RevokeTokenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return new RevokeTokenResponse.Success();
            }
            catch (SecurityTokenException e)
            {
                // return 401 so that the client side can redirect the user to login page
                return new RevokeTokenResponse.Unauthorized(e.Message);
            }
        }
    }
}

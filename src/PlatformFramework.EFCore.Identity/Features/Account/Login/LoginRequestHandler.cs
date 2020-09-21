using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Identity.Abstrations;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using PlatformFramework.Eventing.Decorators.DatabaseRetry;
using PlatformFramework.Eventing.Decorators.Validation;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    [Validation(typeof(LoginRequestValidator))]
    [DatabaseRetry]
    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IClockProvider _clockProvider;
        private readonly IJwtAuthService _jwtAuthManager;
        private readonly ILogger<LoginRequestHandler> _logger;

        public LoginRequestHandler(
             UserManager<User> userManager,
             SignInManager<User> signInManager,
             IClockProvider clockProvider,
             IJwtAuthService jwtAuthService,
             ILogger<LoginRequestHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _clockProvider = clockProvider;
            _jwtAuthManager = jwtAuthService;
            _logger = logger;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager
                .PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, lockoutOnFailure: false)
                .ConfigureAwait(false);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User [{request.UserName}] logged in the system.");

                var appUser = await _userManager.FindByNameAsync(request.UserName).ConfigureAwait(false);
                var claims = await _userManager.GetClaimsAsync(appUser).ConfigureAwait(false);
                var roles = await _userManager.GetRolesAsync(appUser).ConfigureAwait(false);

                claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, appUser.UserName));

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtResult = await _jwtAuthManager.GenerateTokens(request.UserName, claims, _clockProvider.Now);

                return new LoginResponse.Success(new TokenResponse
                {
                    AccessToken = jwtResult.AccessToken
                }, jwtResult.RefreshToken.TokenString);
            }

            if (result.RequiresTwoFactor)
            {
                //return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                return new LoginResponse.NotFound();
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                //return RedirectToAction(nameof(Lockout));
                return new LoginResponse.NotFound();
            }
            else
            {
                return new LoginResponse.BadRequest(request);
            }
        }
    }
}

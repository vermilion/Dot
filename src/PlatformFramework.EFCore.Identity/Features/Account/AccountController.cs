using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Identity.Features.Account;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using System;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IClockProvider _clockProvider;
        private readonly JwtTokenConfig _jwtTokenConfig;

        public AccountController(
            IMediator mediator, 
            IClockProvider clockProvider,
            JwtTokenConfig jwtTokenConfig)
        {
            _mediator = mediator;
            _clockProvider = clockProvider;
            _jwtTokenConfig = jwtTokenConfig;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(LoginRequest))]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var result = await _mediator.Send(model).ConfigureAwait(false);

            switch (result)
            {
                case LoginResponse.Success x:
                    SetTokenCookie(x.RefreshToken);
                    return Ok(x.Model);
                case LoginResponse.NotFound _:
                    return NotFound();
                case LoginResponse.BadRequest x:
                    return BadRequest(x.Model);
                default:
                    throw new Exception("Unknown response type");
            }
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity!.Name!;

            var result = await _mediator.Send(new LogoutRequest(userName)).ConfigureAwait(false);

            return result switch
            {
                LogoutResponse.Success _ => Ok(),
                _ => throw new Exception("Unknown response type")
            };
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var request = new RefreshTokenRequest(refreshToken!);

            var result = await _mediator.Send(request).ConfigureAwait(false);

            switch (result)
            {
                case RefreshTokenResponse.Success x:
                    SetTokenCookie(x.RefreshToken);
                    return Ok(x.Model);
                case RefreshTokenResponse.Unauthorized x:
                    return Unauthorized(x.Model);
                default:
                    throw new Exception("Unknown response type");
            }
        }

        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RevokeTokenResponse))]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
        {
            /*// accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            // users can revoke their own tokens and admins can revoke any tokens
            if (!Account.OwnsToken(token) && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _accountService.RevokeToken(token, ipAddress());*/

            var result = await _mediator.Send(request).ConfigureAwait(false);

            return result switch
            {
                RevokeTokenResponse.Success _ => Ok(),
                RevokeTokenResponse.Unauthorized x => Unauthorized(x.Model),
                _ => throw new Exception("Unknown response type")
            };
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = _clockProvider.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}

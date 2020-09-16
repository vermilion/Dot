using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(LoginRequest))]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var result = await _mediator.Send(model).ConfigureAwait(false);

            return result switch
            {
                LoginResponse.Success x => Ok(x.Model),
                LoginResponse.NotFound _ => NotFound(),
                LoginResponse.BadRequest x => BadRequest(x.Model),
                _ => throw new Exception("Unknown response type")
            };
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

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> RefreshToken([FromQuery] string refreshToken)
        {
            var request = new RefreshTokenRequest(refreshToken)
            {
                UserName = User.Identity!.Name!,
                AccessToken = await HttpContext.GetTokenAsync("Bearer", "access_token").ConfigureAwait(false)
            };

            var result = await _mediator.Send(request).ConfigureAwait(false);

            return result switch
            {
                RefreshTokenResponse.Success x => Ok(x.Model),
                RefreshTokenResponse.Unauthorized x => Unauthorized(x.Model),
                _ => throw new Exception("Unknown response type")
            };
        }
    }
}

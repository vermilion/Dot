using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformFramework.EFCore.Identity.Features.UserLogins.Create;
using PlatformFramework.EFCore.Identity.Features.UserLogins.Delete;
using PlatformFramework.EFCore.Identity.Features.UserLogins.Get;
using PlatformFramework.EFCore.Identity.Features.UserLogins.GetAll;
using PlatformFramework.EFCore.Identity.Features.UserLogins.Update;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using PlatformFramework.Models;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserLoginsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserLoginsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedCollection<UserLoginModel>))]
        public async Task<IActionResult> GetAll([FromQuery] int userId, [FromBody] PagedModel model)
        {
            var request = new GetAllRequest(userId, model);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginModel))]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var request = new GetRequest(id);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginModel))]
        public async Task<IActionResult> Create([FromBody] UserLoginModel model)
        {
            var request = new CreateRequest(model);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginModel))]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UserLoginModel model)
        {
            var request = new UpdateRequest(id, model);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var request = new DeleteRequest(id);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }
    }
}

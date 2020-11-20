using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformFramework.EFCore.Identity.Features.Users.Create;
using PlatformFramework.EFCore.Identity.Features.Users.Delete;
using PlatformFramework.EFCore.Identity.Features.Users.Get;
using PlatformFramework.EFCore.Identity.Features.Users.GetAll;
using PlatformFramework.EFCore.Identity.Features.Users.Update;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using PlatformFramework.Models;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.Users
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedCollection<UserModel>))]
        public async Task<IActionResult> GetAll([FromBody] PagedModel model)
        {
            var request = new GetAllRequest(model);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var request = new GetRequest(id);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        public async Task<IActionResult> Create([FromBody] UserModel model)
        {
            var request = new CreateRequest(model);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UserModel model)
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformFramework.EFCore.Identity.Features.RoleClaims.Create;
using PlatformFramework.EFCore.Identity.Features.RoleClaims.Delete;
using PlatformFramework.EFCore.Identity.Features.RoleClaims.Get;
using PlatformFramework.EFCore.Identity.Features.RoleClaims.GetAll;
using PlatformFramework.EFCore.Identity.Features.RoleClaims.Update;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using PlatformFramework.Models;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RoleClaimsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleClaimsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedCollection<RoleClaimModel>))]
        public async Task<IActionResult> GetAll([FromQuery] int roleId, [FromBody] PagedModel model)
        {
            var request = new GetAllRequest(roleId, model);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleClaimModel))]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var request = new GetRequest(id);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleClaimModel))]
        public async Task<IActionResult> Create([FromBody] RoleClaimModel model)
        {
            var request = new CreateRequest(model);
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleClaimModel))]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] RoleClaimModel model)
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

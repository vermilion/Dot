using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cofoundry.Web.Admin
{
    public class PermissionsApiController : BaseApiController
    {
        private readonly IMediator _mediator;

        public PermissionsApiController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.ExecuteAsync(new GetAllPermissionsQuery());
            return Ok(results);
        }
    }
}
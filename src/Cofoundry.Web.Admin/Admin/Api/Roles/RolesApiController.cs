using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class RolesApiController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RolesApiController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        #region queries

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] SearchRolesQuery query)
        {
            if (query == null) query = new SearchRolesQuery();

            var results = await _mediator.ExecuteAsync(query);
            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int roleId)
        {
            var result = await _mediator.ExecuteAsync(new GetRoleDetailsByIdQuery(roleId));
            return Ok(result);
        }

        #endregion

        #region commands

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRoleCommand model)
        {
            await _mediator.ExecuteAsync(model);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateRoleCommand model)
        {
            await _mediator.ExecuteAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int roleId)
        {
            var request = new DeleteRoleCommand
            {
                RoleId = roleId
            };

            await _mediator.ExecuteAsync(request);
            return Ok();
        }

        #endregion
    }
}
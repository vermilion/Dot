using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class AccountApiController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;

        public AccountApiController(
            IMediator mediator,
            IUserContextService userContextService
            )
        {
            _mediator = mediator;
            _userContextService = userContextService;
        }

        #region queries

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetCurrentUserAccountDetailsQuery();

            var result = await _mediator.ExecuteAsync(query);
            return Ok(result);
        }
        
        #endregion

        #region commands

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateCurrentUserAccountCommand model)
        {
            await _mediator.ExecuteAsync(model);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PutPassword([FromBody] UpdateCurrentUserPasswordCommandDto model)
        {
            var command = new UpdateCurrentUserPasswordCommand()
            {
                OldPassword = model.OldPassword,
                NewPassword = model.NewPassword
            };

            await _mediator.ExecuteAsync(command);
            return Ok();
        }

        #endregion
    }
}
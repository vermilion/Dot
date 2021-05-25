using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class UsersApiController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UsersApiController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        #region queries

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] SearchUserSummariesQuery query)
        {
            if (query == null) query = new SearchUserSummariesQuery();

            var results = await _mediator.ExecuteAsync(query);
            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int userId)
        {
            var query = new GetUserDetailsByIdQuery(userId);
            var result = await _mediator.ExecuteAsync(query);

            return Ok(result);
        }

        #endregion

        #region commands

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserCommand command)
        {
            // TODO: We have a separate command here for adding Cofoundry Admin users, but we could re-use the same one
            // and separate the notification part out of the handler and make it a separate function in the admin panel.
            var userCommand = new AddCofoundryUserCommand
            {
                Username = command.Username,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                RoleId = command.RoleId
            };

            var result = await _mediator.ExecuteAsync(userCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand delta)
        {
            await _mediator.ExecuteAsync(delta);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int userId)
        {
            var command = new DeleteUserCommand
            {
                UserId = userId
            };

            await _mediator.ExecuteAsync(command);
            return Ok();
        }

        #endregion
    }
}
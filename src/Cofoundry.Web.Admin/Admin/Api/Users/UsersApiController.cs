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
        public async Task<IActionResult> Add([FromBody] UserSummary model)
        {
            // TODO: We have a separate command here for adding Cofoundry Admin users, but we could re-use the same one
            // and separate the notification part out of the handler and make it a separate function in the admin panel.
            var userCommand = new AddCofoundryUserCommand
            {
                Username = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleId = model.Role.RoleId
            };

            var result = await _mediator.ExecuteAsync(userCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UserSummary model)
        {
            var command = new UpdateUserCommand
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Username = model.Username,
                RequirePasswordChange = false,

                RoleId = model.Role.RoleId
            };

            await _mediator.ExecuteAsync(command);
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
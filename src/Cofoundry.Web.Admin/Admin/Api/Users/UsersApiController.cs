using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class UsersApiController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IApiResponseHelper _apiResponseHelper;

        public UsersApiController(
            IMediator mediator,
            IApiResponseHelper apiResponseHelper
            )
        {
            _mediator = mediator;
            _apiResponseHelper = apiResponseHelper;
        }

        #region queries

        [HttpPost]
        public async Task<JsonResult> GetAll([FromBody] SearchUserSummariesQuery query)
        {
            if (query == null) query = new SearchUserSummariesQuery();

            var results = await _mediator.ExecuteAsync(query);
            return _apiResponseHelper.SimpleQueryResponse(results);
        }

        [HttpGet]
        public async Task<JsonResult> GetById(int userId)
        {
            var query = new GetUserDetailsByIdQuery(userId);
            var result = await _mediator.ExecuteAsync(query);

            return _apiResponseHelper.SimpleQueryResponse(result);
        }

        #endregion

        #region commands

        [HttpPost]
        public async Task<JsonResult> Add([FromBody] AddUserCommand command)
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

            return await _apiResponseHelper.RunCommandAsync(userCommand);
        }

        [HttpPost]
        public Task<JsonResult> Update([FromBody] UpdateUserCommand delta)
        {
            return _apiResponseHelper.RunCommandAsync(delta);
        }

        [HttpDelete]
        public Task<JsonResult> Delete(int userId)
        {
            var command = new DeleteUserCommand
            {
                UserId = userId
            };

            return _apiResponseHelper.RunCommandAsync(command);
        }

        #endregion
    }
}
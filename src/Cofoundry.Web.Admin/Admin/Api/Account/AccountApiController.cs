using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class AccountApiController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IApiResponseHelper _apiResponseHelper;
        private readonly IUserContextService _userContextService;

        public AccountApiController(
            IMediator mediator,
            IApiResponseHelper apiResponseHelper,
            IUserContextService userContextService
            )
        {
            _mediator = mediator;
            _apiResponseHelper = apiResponseHelper;
            _userContextService = userContextService;
        }

        #region queries

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var query = new GetCurrentUserAccountDetailsQuery();

            var results = await _mediator.ExecuteAsync(query);
            return _apiResponseHelper.SimpleQueryResponse(results);
        }
        
        #endregion

        #region commands

        [HttpPost]
        public async Task<JsonResult> Update([FromBody] UpdateCurrentUserAccountCommand delta)
        {
            return await _apiResponseHelper.RunCommandAsync(delta);
        }

        [HttpPost]
        public Task<JsonResult> PutPassword([FromBody] UpdateCurrentUserPasswordCommandDto dto)
        {
            var command = new UpdateCurrentUserPasswordCommand()
            {
                OldPassword = dto.OldPassword,
                NewPassword = dto.NewPassword
            };

            return _apiResponseHelper.RunCommandAsync(command);
        }

        #endregion
    }
}
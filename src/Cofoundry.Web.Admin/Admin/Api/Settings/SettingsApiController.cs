using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class SettingsApiController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IApiResponseHelper _apiResponseHelper;

        public SettingsApiController(
            IMediator mediator,
            IApiResponseHelper apiResponseHelper
            )
        {
            _mediator = mediator;
            _apiResponseHelper = apiResponseHelper;
        }

        #region queries

        [HttpGet]
        public async Task<IActionResult> GetGeneralSiteSettings()
        {
            var results = await _mediator.ExecuteAsync(new GetSettingsQuery<GeneralSiteSettings>());
            return _apiResponseHelper.SimpleQueryResponse(results);
        }

        #endregion

        #region commands

        [HttpPost]
        public Task<JsonResult> UpdateGeneralSiteSettings([FromBody] UpdateGeneralSiteSettingsCommand delta)
        {
            return _apiResponseHelper.RunCommandAsync(delta);
        }

        #endregion
    }
}
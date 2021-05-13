using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class SettingsApiController : BaseApiController
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IApiResponseHelper _apiResponseHelper;

        public SettingsApiController(
            IQueryExecutor queryExecutor,
            IApiResponseHelper apiResponseHelper
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
        }

        #region queries

        [HttpGet]
        public async Task<IActionResult> GetGeneralSiteSettings()
        {
            var results = await _queryExecutor.ExecuteAsync(new GetSettingsQuery<GeneralSiteSettings>());
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
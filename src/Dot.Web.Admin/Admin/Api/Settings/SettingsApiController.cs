using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class SettingsApiController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SettingsApiController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        #region queries

        [HttpGet]
        public async Task<IActionResult> GetGeneralSiteSettings()
        {
            var results = await _mediator.ExecuteAsync(new GetSettingsQuery<GeneralSiteSettings>());
            return Ok(results);
        }

        #endregion

        #region commands

        [HttpPost]
        public async Task<IActionResult> UpdateGeneralSiteSettings([FromBody] UpdateGeneralSiteSettingsCommand delta)
        {
            await _mediator.ExecuteAsync(delta);
            return Ok();
        }

        #endregion
    }
}
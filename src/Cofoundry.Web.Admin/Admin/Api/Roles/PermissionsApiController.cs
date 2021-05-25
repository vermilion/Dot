using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Web.Admin
{
    public class PermissionsApiController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IApiResponseHelper _apiResponseHelper;

        public PermissionsApiController(
            IMediator mediator,
            IApiResponseHelper apiResponseHelper
            )
        {
            _mediator = mediator;
            _apiResponseHelper = apiResponseHelper;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var results = await _mediator.ExecuteAsync(new GetAllPermissionsQuery());
            return _apiResponseHelper.SimpleQueryResponse(results);
        }
    }
}
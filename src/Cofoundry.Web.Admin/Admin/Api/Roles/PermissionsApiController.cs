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
        private readonly IQueryExecutor _queryExecutor;
        private readonly IApiResponseHelper _apiResponseHelper;

        public PermissionsApiController(
            IQueryExecutor queryExecutor,
            IApiResponseHelper apiResponseHelper
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var results = await _queryExecutor.ExecuteAsync(new GetAllPermissionsQuery());
            return _apiResponseHelper.SimpleQueryResponse(results);
        }
    }
}
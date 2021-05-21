using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class RolesApiController : BaseApiController
    {
        private readonly IQueryExecutor _queryExecutor;
        private readonly IApiResponseHelper _apiResponseHelper;

        public RolesApiController(
            IQueryExecutor queryExecutor,
            IApiResponseHelper apiResponseHelper
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
        }

        #region queries

        [HttpPost]
        public async Task<JsonResult> GetAll([FromBody] SearchRolesQuery query)
        {
            if (query == null) query = new SearchRolesQuery();

            var results = await _queryExecutor.ExecuteAsync(query);
            return _apiResponseHelper.SimpleQueryResponse(results);
        }

        [HttpGet]
        public async Task<JsonResult> GetById(int roleId)
        {
            var result = await _queryExecutor.ExecuteAsync(new GetRoleDetailsByIdQuery(roleId));
            return _apiResponseHelper.SimpleQueryResponse(result);
        }

        #endregion

        #region commands

        [HttpPost]
        public Task<JsonResult> Add([FromBody] AddRoleCommand command)
        {
            return _apiResponseHelper.RunCommandAsync(command);
        }

        [HttpPost]
        public Task<JsonResult> Update([FromBody] UpdateRoleCommand delta)
        {
            return _apiResponseHelper.RunCommandAsync(delta);
        }

        [HttpDelete]
        public Task<JsonResult> Delete(int roleId)
        {
            var command = new DeleteRoleCommand
            {
                RoleId = roleId
            };

            return _apiResponseHelper.RunCommandAsync(command);
        }

        #endregion
    }
}
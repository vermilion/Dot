using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Service to validate the model of command/query handler prior to execution.
    /// </summary>
    public class ExecuteModelValidationService : IExecuteModelValidationService
    {
        #region constructor

        private readonly IModelValidationService _modelValidationService;

        public ExecuteModelValidationService(
            IModelValidationService modelValidationService
            )
        {
            _modelValidationService = modelValidationService;
        }

        #endregion

        public virtual void Validate<TRequest, TResponse>(TRequest query, IRequestHandler<TRequest, TResponse> queryHandler, IExecutionContext executionContext) where TRequest : IRequest<TResponse>
        {
            _modelValidationService.Validate(query);

            /*if (queryHandler is IPermissionRestrictedQueryHandler<TQuery, TResult>)
            {
                var permissions = ((IPermissionRestrictedQueryHandler<TQuery, TResult>)queryHandler).GetPermissions(query);
                _modelValidationService.EnforcePermission(permissions, executionContext.UserContext);
            }*/
        }
    }
}

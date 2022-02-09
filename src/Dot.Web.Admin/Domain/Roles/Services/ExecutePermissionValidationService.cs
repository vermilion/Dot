using System;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Service to validate the permissions of command/query handler prior to execution.
    /// </summary>
    public class ExecutePermissionValidationService : IExecutePermissionValidationService
    {
        #region constructor

        private readonly IPermissionValidationService _permissionValidationService;

        public ExecutePermissionValidationService(
            IPermissionValidationService permissionValidationService
            )
        {
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        public virtual void Validate<TQuery, TResult>(TQuery query, IRequestHandler<TQuery, TResult> queryHandler, IExecutionContext executionContext) where TQuery : IRequest<TResult>
        {
            ValidateQueryImplementation<TQuery, TResult>(queryHandler);

            if (queryHandler is IPermissionRestrictedRequestHandler<TQuery>)
            {
                var permissions = ((IPermissionRestrictedRequestHandler<TQuery>)queryHandler).GetPermissions(query);
                _permissionValidationService.EnforcePermission(permissions, executionContext.UserContext);
            }
        }

        #region private

        protected void ValidateQueryImplementation<TQuery, TResult>(object handler)
            where TQuery : IRequest<TResult>
        {
            if (handler is IPermissionRestrictedRequestHandler)
            {
                // Check for invalid implementation
                if (!(handler is IPermissionRestrictedRequestHandler<TQuery>))
                {
                    var msg = string.Format("Invalid implementation: {0} imeplements IPermissionRestrictedCommandHandler but not IPermissionRestrictedCommandHandler<{1}>", handler.GetType().FullName, typeof(TQuery).FullName);
                    throw new InvalidOperationException(msg);
                }
            }
        }

        #endregion
    }
}

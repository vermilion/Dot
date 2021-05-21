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

        public virtual void Validate<TCommand>(TCommand command, ICommandHandler<TCommand> commandHandler, IExecutionContext executionContext) where TCommand : ICommand
        {
            ValidateCommmandImplementation<TCommand>(commandHandler);

            if (commandHandler is IPermissionRestrictedCommandHandler<TCommand>)
            {
                var permissions = ((IPermissionRestrictedCommandHandler<TCommand>)commandHandler).GetPermissions(command);
                _permissionValidationService.EnforcePermission(permissions, executionContext.UserContext);
            }
        }

        public virtual void Validate<TQuery, TResult>(TQuery query, IQueryHandler<TQuery, TResult> queryHandler, IExecutionContext executionContext) where TQuery : IQuery<TResult>
        {
            ValidateQueryImplementation<TQuery, TResult>(queryHandler);

            if (queryHandler is IPermissionRestrictedQueryHandler<TQuery, TResult>)
            {
                var permissions = ((IPermissionRestrictedQueryHandler<TQuery, TResult>)queryHandler).GetPermissions(query);
                _permissionValidationService.EnforcePermission(permissions, executionContext.UserContext);
            }
        }

        #region private

        protected void ValidateCommmandImplementation<TCommand>(object handler)
            where TCommand : ICommand
        {
            if (handler is IPermissionRestrictedCommandHandler)
            {
                // Check for invalid implementation
                if (!(handler is IPermissionRestrictedCommandHandler<TCommand>))
                {
                    var msg = string.Format("Invalid implementation: {0} implements IPermissionRestrictedCommandHandler but not IPermissionRestrictedCommandHandler<{1}>", handler.GetType().FullName, typeof(TCommand).FullName);
                    throw new InvalidOperationException(msg);
                }
            }
        }

        protected void ValidateQueryImplementation<TQuery, TResult>(object handler)
            where TQuery : IQuery<TResult>
        {
            if (handler is IPermissionRestrictedQueryHandler)
            {
                // Check for invalid implementation
                if (!(handler is IPermissionRestrictedQueryHandler<TQuery, TResult>))
                {
                    var msg = string.Format("Invalid implementation: {0} imeplements IPermissionRestrictedCommandHandler but not IPermissionRestrictedCommandHandler<{1}>", handler.GetType().FullName, typeof(TQuery).FullName);
                    throw new InvalidOperationException(msg);
                }
            }
        }

        #endregion
    }
}

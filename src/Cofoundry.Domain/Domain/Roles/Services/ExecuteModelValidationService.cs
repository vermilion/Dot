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

        public virtual void Validate<TCommand>(TCommand command, ICommandHandler<TCommand> commandHandler, IExecutionContext executionContext) where TCommand : ICommand
        {
            _modelValidationService.Validate(command);

            /*if (commandHandler is IPermissionRestrictedCommandHandler<TCommand>)
            {
                var permissions = ((IPermissionRestrictedCommandHandler<TCommand>)commandHandler).GetPermissions(command);
                _modelValidationService.EnforcePermission(permissions, executionContext.UserContext);
            }*/
        }

        public virtual void Validate<TQuery, TResult>(TQuery query, IQueryHandler<TQuery, TResult> queryHandler, IExecutionContext executionContext) where TQuery : IQuery<TResult>
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

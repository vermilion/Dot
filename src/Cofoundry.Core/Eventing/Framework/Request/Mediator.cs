using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Cofoundry.Core.Validation;

namespace Cofoundry.Domain.CQS.Internal
{
    /// <summary>
    /// Handles the execution IRequest instances.
    /// </summary>
    public class Mediator : IMediator
    {
        private static readonly MethodInfo _executeAsyncMethod = typeof(Mediator).GetMethod(nameof(ExecuteRequestAsync), BindingFlags.NonPublic | BindingFlags.Instance);
        
        #region constructor

        private readonly IRequestHandlerFactory _queryHandlerFactory;
        private readonly IExecutionContextFactory _executionContextFactory;
        private readonly IExecuteModelValidationService _executeModelValidationService;
        private readonly IExecutePermissionValidationService _executePermissionValidationService;

        public Mediator(
            IModelValidationService modelValidationService,
            IRequestHandlerFactory queryHandlerFactory,
            IExecutionContextFactory executionContextFactory,
            IExecuteModelValidationService executeModelValidationService,
            IExecutePermissionValidationService executePermissionValidationService
            )
        {
            _queryHandlerFactory = queryHandlerFactory;
            _executionContextFactory = executionContextFactory;
            _executeModelValidationService = executeModelValidationService;
            _executePermissionValidationService = executePermissionValidationService;
        }

        #endregion

        #region async execution

        /// <summary>
        /// Handles the asynchronous execution the specified query.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        /// <param name="executionContext">
        /// Optional custom execution context which can be used to impersonate/elevate permissions 
        /// or change the execution date.
        /// </param>
        public async Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> query, IExecutionContext executionContext = null)
        {
            TResult result;

            if (query == null) 
                return default;
            
            try
            {
                result = await (Task<TResult>)_executeAsyncMethod
                    .MakeGenericMethod(query.GetType(), typeof(TResult))
                    .Invoke(this, new object[] { query, executionContext });
            }
            catch (TargetInvocationException ex)
            {
                result = HandleException<TResult>(ex);
            }

            return result;
        }

        private async Task<TResponse> ExecuteRequestAsync<TRequest, TResponse>(TRequest request, IExecutionContext executionContext) 
            where TRequest : IRequest<TResponse>
        {
            if (request == null) 
                return default;

            var ctx = await CreateExecutionContextAsync(executionContext);

            var handler = _queryHandlerFactory.CreateAsyncHandler<TRequest, TResponse>();
            if (handler == null)
            {
                throw new MissingHandlerMappingException(typeof(TRequest));
            }

            await _executeModelValidationService.Validate(request, handler, ctx);
            _executePermissionValidationService.Validate(request, handler, ctx);
            var result = await handler.ExecuteAsync(request, ctx);

            return result;
        }

        #endregion

        #region helpers

        private async Task<IExecutionContext> CreateExecutionContextAsync(IExecutionContext cx)
        {
            if (cx == null)
            {
                return await _executionContextFactory.CreateAsync();
            }

            if (cx.UserContext == null)
            {
                throw new ExecutionContextNotInitializedException("The UserContext property cannot be null");
            }

            if (cx.ExecutionDate == DateTime.MinValue)
            {
                throw new ExecutionContextNotInitializedException("The ExecutionDate property has not been set");
            }

            return cx;
        }

        private TResult HandleException<TResult>(TargetInvocationException ex)
        {
            var info = ExceptionDispatchInfo.Capture(ex.InnerException);
            info.Throw();

            // compiler requires assignment
            return default;
        }

        #endregion
    }
}

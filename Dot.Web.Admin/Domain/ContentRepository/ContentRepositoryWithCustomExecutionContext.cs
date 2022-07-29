using System;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain.Internal
{
    public class ContentRepositoryWithCustomExecutionContext
        : ContentRepository
        , IContentRepositoryWithCustomExecutionContext
    {
        private readonly IMediator _mediator;
        private IExecutionContext _executionContext = null;

        public ContentRepositoryWithCustomExecutionContext(
            IServiceProvider serviceProvider,
            IMediator mediator
            )
            : base(serviceProvider, mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sets the execution context for any queries or commands
        /// chained of this instance. Typically used to impersonate
        /// a user or elevate permissions.
        /// </summary>
        /// <param name="executionContext">
        /// The execution context instance to use. May pass null to reset 
        /// this instance and use the default.
        /// </param>
        public virtual void SetExecutionContext(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        /// <summary>
        /// Handles the asynchronous execution the specified query.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        public override Task<TResult> ExecuteRequestAsync<TResult>(IRequest<TResult> query)
        {
            return _mediator.ExecuteAsync(query, _executionContext);
        }
    }
}

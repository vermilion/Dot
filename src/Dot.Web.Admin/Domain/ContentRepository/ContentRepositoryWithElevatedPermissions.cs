using Cofoundry.Domain.CQS;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    public class ContentRepositoryWithElevatedPermissions 
        : ContentRepository
        , IContentRepositoryWithElevatedPermissions
    {
        private readonly IMediator _mediator;
        private readonly Lazy<Task<IExecutionContext>> _elevatedExecutionContextAsync;
        
        public ContentRepositoryWithElevatedPermissions(
            IServiceProvider serviceProvider,
            IMediator mediator,
            IExecutionContextFactory executionContextFactory
            )
            : base(serviceProvider, mediator)
        {
            _mediator = mediator;

            _elevatedExecutionContextAsync = new Lazy<Task<IExecutionContext>>(() =>
            {
                return executionContextFactory.CreateSystemUserExecutionContextAsync();
            });
        }

        /// <summary>
        /// Handles the asynchronous execution the specified query.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        public override async Task<TResult> ExecuteRequestAsync<TResult>(IRequest<TResult> query)
        {
            var executionContext = await _elevatedExecutionContextAsync.Value;
            return await _mediator.ExecuteAsync(query, executionContext);
        }
    }
}

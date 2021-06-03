using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Extendable;
using System;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    public class ContentRepository 
        : IContentRepository
        , IAdvancedContentRepository
        , IExtendableContentRepository
    {
        private readonly IMediator _mediator;

        public ContentRepository(
            IServiceProvider serviceProvider,
            IMediator mediator
            )
        {
            ServiceProvider = serviceProvider;
            _mediator = mediator;
        }

        /// <summary>
        /// Service provider instance to be used for extension only
        /// i.e. by internal Cofoundry or plugins. Access this by casting
        /// to the IExtendableContentRepository interface.
        /// </summary>
        public virtual IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Handles the asynchronous execution the specified query.
        /// </summary>
        /// <param name="query">Query to execute.</param>
        public virtual Task<TResult> ExecuteRequestAsync<TResult>(IRequest<TResult> query)
        {
            return _mediator.ExecuteAsync(query);
        }
    }
}

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cofoundry.Domain.CQS.Internal
{
    /// <summary>
    /// Factory to create the default RequestHandler instance
    /// </summary>
    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestHandlerFactory(
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates a new IAsyncQueryHandler instance with the specified type signature.
        /// </summary>
        public IRequestHandler<TQuery, TResult> CreateAsyncHandler<TQuery, TResult>() where TQuery : IRequest<TResult>
        {
            return _serviceProvider.GetRequiredService<IRequestHandler<TQuery, TResult>>();
        }
    }
}

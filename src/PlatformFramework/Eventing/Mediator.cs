using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using PlatformFramework.Eventing.Helpers;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing
{
    /// <summary>
    /// Default mediator implementation relying on single- and multi instance delegates for resolving handlers.
    /// </summary>
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator"/> class.
        /// </summary>
        /// <param name="serviceProvider">The single instance factory.</param>
        /// <param name="loggerFactory"></param>
        public Mediator(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider;

            _logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
               .CreateLogger(GetType().Name);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(request, nameof(request));

            var requestType = request.GetType();

            object addFunc(Type t)
            {
                var instance = Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(requestType, typeof(TResponse)));
                return instance ?? throw new NullReferenceException("Unable to create internal handler wrapper");
            }

            var handler = (RequestHandlerWrapper<TResponse>)_requestHandlers.GetOrAdd(requestType, addFunc);

            handler.InitializeLogger(_logger);
            return handler.Handle(request, _serviceProvider, cancellationToken);
        }
    }
}

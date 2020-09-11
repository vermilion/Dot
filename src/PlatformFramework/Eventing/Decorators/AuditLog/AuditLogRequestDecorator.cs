using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing.Decorators.AuditLog
{
    [Mapping(typeof(AuditLogAttribute))]
    public class AuditLogRequestDecorator<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        private readonly IRequestHandler<TRequest, TResult> _handler;
        private readonly ILogger _logger;

        public AuditLogRequestDecorator(IRequestHandler<TRequest, TResult> handler, ILoggerFactory loggerFactory)
        {
            _handler = handler;

            _logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
                .CreateLogger(GetType().Name);
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Executing request of type {@RequestType}: {@Request}", request.GetType().Name, request);

            var response = await _handler.Handle(request, cancellationToken).ConfigureAwait(false);

            _logger.LogDebug("Executed request: {@Result}", response);

            return response;
        }
    }
}
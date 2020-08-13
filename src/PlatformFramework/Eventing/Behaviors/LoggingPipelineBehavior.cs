using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PlatformFramework.Eventing.Behaviors
{
    [DebuggerStepThrough]
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IRequest<TResponse>
    {
        private readonly ILogger _logger;

        public LoggingPipelineBehavior(ILoggerFactory loggerFactory)
        {
            _logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
                 .CreateLogger(GetType().Name);
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug("Executing request: {@Request}", request);

            var response = await next();

            _logger.LogWarning("Executed request: {@Result}", response);

            return response;
        }
    }
}

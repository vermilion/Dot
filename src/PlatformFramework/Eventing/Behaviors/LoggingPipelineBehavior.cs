using MediatR;
using Microsoft.Extensions.Logging;
using PlatformFramework.Shared.GuardToolkit;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.UseCases.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingPipelineBehavior(ILoggerFactory loggerFactory)
        {
            _logger = Ensure
                 .IsNotNull(loggerFactory, nameof(loggerFactory))
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

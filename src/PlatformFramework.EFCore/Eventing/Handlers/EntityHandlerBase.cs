using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.Eventing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Eventing.Handlers
{
    /// <summary>
    /// Base class for implementing Entity CRUD scenarios
    /// </summary>
    /// <typeparam name="TRequest">Unique request type</typeparam>
    /// <typeparam name="TResponse">Response type</typeparam>
    public abstract class EntityHandlerBase<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Current instalce logger <see cref="ILogger"/>
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Unit of work <see cref="IUnitOfWork"/>
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        protected EntityHandlerBase(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            Logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
                .CreateLogger(GetType().Name);

            UnitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
        }
    }
}

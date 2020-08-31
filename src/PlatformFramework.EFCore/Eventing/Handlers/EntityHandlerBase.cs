using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Abstractions;
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
    public abstract class EntityHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
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

        /// <summary>
        /// Mapper <see cref="IMapper"/>
        /// </summary>
        protected IMapper Mapper { get; }

        protected EntityHandlerBase(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            Logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
                .CreateLogger(GetType().Name);

            UnitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
            Mapper = Guard.Against.Null(mapper, nameof(mapper));
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

using Ardalis.GuardClauses;
using MediatR;

namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base entity request type by model
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntityModelRequest<TReadModel> : IRequest<TReadModel>
    {
        protected EntityModelRequest(TReadModel model)
        {
            Guard.Against.Null(model, nameof(model));
            Model = model;
        }

        public TReadModel Model { get; }
    }
}

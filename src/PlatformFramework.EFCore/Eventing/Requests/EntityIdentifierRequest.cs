using MediatR;

namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base entity request type by identitier
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntityIdentifierRequest<TReadModel> : IRequest<TReadModel>
    {
        protected EntityIdentifierRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}

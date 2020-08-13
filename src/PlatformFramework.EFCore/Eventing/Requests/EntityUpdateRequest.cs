namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base update entity request type
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntityUpdateRequest<TReadModel> : EntityModelRequest<TReadModel>
    {
        public EntityUpdateRequest(long id, TReadModel model) : base(model)
        {
            Id = id;
        }

        public long Id { get; }
    }
}

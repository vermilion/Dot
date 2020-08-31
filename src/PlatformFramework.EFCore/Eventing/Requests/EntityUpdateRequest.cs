namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base update entity request type
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntityUpdateRequest<TReadModel> : EntityModelRequest<TReadModel>
    {
        protected EntityUpdateRequest(int id, TReadModel model)
            : base(model)
        {
            Id = id;
        }

        public int Id { get; }
    }
}

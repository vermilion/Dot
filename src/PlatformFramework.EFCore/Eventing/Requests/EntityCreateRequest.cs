namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base create entity request type
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntityCreateRequest<TReadModel> : EntityModelRequest<TReadModel>
    {
        public EntityCreateRequest(TReadModel model) : base(model)
        {
        }
    }
}

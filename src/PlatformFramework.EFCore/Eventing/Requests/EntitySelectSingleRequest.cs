namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base select entity request type
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntitySelectSingleRequest<TReadModel> : EntityIdentifierRequest<TReadModel>
        where TReadModel : class
    {
        protected EntitySelectSingleRequest(int id)
            : base(id)
        {
        }
    }
}

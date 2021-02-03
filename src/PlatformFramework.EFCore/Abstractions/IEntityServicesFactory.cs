namespace PlatformFramework.EFCore.Abstractions
{
    /// <summary>
    /// Defines the interfaces for <see cref="IEntityService{TEntity}"/> interfaces.
    /// </summary>
    public interface IEntityServicesFactory
    {
        /// <summary>
        /// Gets the specified service for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IEntityService{TEntity}"/> interface.</returns>
        IEntityService<TEntity> GetEntityServiceFor<TEntity>() where TEntity : class, IEntity, new();
    }
}
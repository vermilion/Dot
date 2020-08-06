using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Context.Hooks
{
    /// <summary>
    /// A strongly typed PreActionHook.
    /// </summary>
    public abstract class DbContextEntityHook : IDbContextEntityHook
    {
        /// <summary>
        /// Entity States that this hook must be registered to listen for.
        /// </summary>
        public abstract EntityState HookState { get; }

        /// <summary>
        /// The logic to perform per entity before the registered action gets performed.
        /// This gets run once per entity that has been changed.
        /// </summary>
        public abstract Task BeforeSaveChanges(object entity, HookEntityMetadata metadata);

        /// <summary>
        /// The logic to perform per entity before the registered action gets performed.
        /// This gets run once per entity that has been changed.
        /// </summary>
        public abstract Task AfterSaveChanges(object entity, HookEntityMetadata metadata);

        public abstract bool CanHook(EntityConfigFlags flags);
    }

    /// <summary>
    /// Implements a strongly-typed hook to be run after an action is performed in the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity this hook must watch for.</typeparam>
    public abstract class DbContextEntityHook<TEntity> : IDbContextEntityHook
    {
        /// <summary>
        /// Implements the interface.  This causes the hook to only run for objects that are assignable to TEntity.
        /// </summary>
        public async Task BeforeSaveChanges(object entity, HookEntityMetadata metadata)
        {
            if (entity is TEntity typedEntity)
                await BeforeSaveChanges(typedEntity, metadata);
        }

        /// <summary>
        /// Implements the interface.  This causes the hook to only run for objects that are assignable to TEntity.
        /// </summary>
        public async Task AfterSaveChanges(object entity, HookEntityMetadata metadata)
        {
            if (entity is TEntity typedEntity)
                await AfterSaveChanges(typedEntity, metadata);
        }

        /// <summary>
        /// Entity States that this hook must be registered to listen for.
        /// </summary>
        public abstract EntityState HookState { get; }

        /// <summary>
        /// The logic to perform per entity after the registered action gets performed.
        /// This gets run once per entity that has been changed.
        /// </summary>
        protected abstract Task BeforeSaveChanges(TEntity entity, HookEntityMetadata metadata);

        /// <summary>
        /// The logic to perform per entity after the registered action gets performed.
        /// This gets run once per entity that has been changed.
        /// </summary>
        protected abstract Task AfterSaveChanges(TEntity entity, HookEntityMetadata metadata);

        public abstract bool CanHook(EntityConfigFlags flags);
    }
}
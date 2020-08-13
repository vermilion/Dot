using Microsoft.EntityFrameworkCore;

namespace PlatformFramework.EFCore.Context.Hooks
{
    /// <summary>
    /// Implements a hook that will run after an entity gets updated in the database.
    /// </summary>
    public abstract class EntityUpdatedHook<TEntity> : EntityHook<TEntity>
    {
        /// <summary>
        /// Returns <see cref="EntityState.Modified"/> as the hookState to listen for.
        /// </summary>
        public override EntityState HookState => EntityState.Modified;
    }
}
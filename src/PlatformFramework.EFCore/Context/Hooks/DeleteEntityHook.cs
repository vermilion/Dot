using Microsoft.EntityFrameworkCore;

namespace PlatformFramework.EFCore.Context.Hooks
{
    /// <summary>
    /// Implements a hook that will run after an entity gets deleted from the database.
    /// </summary>
    public abstract class DeleteEntityHook<TEntity> : EntityHook<TEntity>
    {
        /// <summary>
        /// Returns <see cref="EntityState.Deleted"/> as the hookState to listen for.
        /// </summary>
        public override EntityState HookState => EntityState.Deleted;
    }
}
using Microsoft.EntityFrameworkCore;

namespace PlatformFramework.EFCore.Context.Hooks
{
    /// <summary>
    /// Implements a hook that will run after an entity gets inserted into the database.
    /// </summary>
    public abstract class InsertEntityHook<TEntity> : EntityHook<TEntity>
    {
        /// <summary>
        /// Returns <see cref="EntityState.Added"/> as the hookState to listen for.
        /// </summary>
        public override EntityState HookState => EntityState.Added;
    }
}
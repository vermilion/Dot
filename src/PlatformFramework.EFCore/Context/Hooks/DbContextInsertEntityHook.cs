using Microsoft.EntityFrameworkCore;

namespace PlatformFramework.EFCore.Context.Hooks
{
    /// <summary>
    /// Implements a hook that will run after an entity gets inserted into the database.
    /// </summary>
    public abstract class DbContextInsertEntityHook : DbContextEntityHook
    {
        /// <summary>
        /// Returns <see cref="EntityState.Added"/> as the hookState to listen for.
        /// </summary>
        public override EntityState HookState => EntityState.Added;
    }

    /// <summary>
    /// Implements a hook that will run after an entity gets inserted into the database.
    /// </summary>
    public abstract class DbContextInsertEntityHook<TEntity> : DbContextEntityHook<TEntity>
    {
        /// <summary>
        /// Returns <see cref="EntityState.Added"/> as the hookState to listen for.
        /// </summary>
        public override EntityState HookState => EntityState.Added;
    }
}
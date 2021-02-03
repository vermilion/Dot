using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Context.Extensions;
using PlatformFramework.EFCore.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Context
{
    /// <summary>
    /// Framework's DbContext encapsulating all the entities/hooks/etc.. logic
    /// </summary>
    public abstract class DbContextCore : DbContext
    {
        protected DbContextCore(DbContextOptions options)
            : base(options)
        {
        }

        public sealed override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesWithHooks(OnSaveCompleted, cancellationToken);
        }

        public sealed override int SaveChanges()
        {
            return this.SaveChangesWithHooks(OnSaveCompleted).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Callback executed once save complete
        /// </summary>
        /// <param name="context">Context <see cref="EntityChangeContext"/></param>
        protected virtual void OnSaveCompleted(EntityChangeContext context)
        {
        }
    }
}
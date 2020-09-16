using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Entities;
using PlatformFramework.EFCore.Identity.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Context
{
    /// <summary>
    /// Framework's DbContext encapsulating all the entities/hooks/etc.. logic
    /// </summary>
    public abstract class IdentityDbContextCore : IdentityDbContext<User, Role, int>
    {
        protected IdentityDbContextCore(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyEntitiesConfiguration(this);
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
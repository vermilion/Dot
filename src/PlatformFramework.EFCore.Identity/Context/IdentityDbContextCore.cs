using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Identity.Context.Configurations;

namespace PlatformFramework.EFCore.Identity.Context
{
    /// <summary>
    /// Framework's DbContext encapsulating all the entities/hooks/etc.. logic
    /// </summary>
    public abstract class IdentityDbContextCore : DbContextCore
    {
        protected IdentityDbContextCore(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
        }
    }
}
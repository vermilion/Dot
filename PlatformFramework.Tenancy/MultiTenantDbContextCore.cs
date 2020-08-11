using System.Threading;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore;
using PlatformFramework.EFCore.Context;

namespace PlatformFramework.Tenancy
{
    /// <summary>
    /// Framework's DbContext encapsulating all the entities/hooks/etc.. logic
    /// </summary>
    public abstract class MultiTenantDbContextCore : DbContextCore, IMultiTenantDbContext
    {
        public ITenantInfo TenantInfo { get; }

        public TenantMismatchMode TenantMismatchMode { get; set; } = TenantMismatchMode.Throw;

        public TenantNotSetMode TenantNotSetMode { get; set; } = TenantNotSetMode.Throw;

        protected MultiTenantDbContextCore(ITenantInfo tenantInfo, DbContextOptions options)
            : base(options)
        {
            TenantInfo = tenantInfo;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureMultiTenant();

            modelBuilder.ApplyEntitiesConfiguration(this);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.EnforceMultiTenant();
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            this.EnforceMultiTenant();
            return base.SaveChanges();
        }
    }
}
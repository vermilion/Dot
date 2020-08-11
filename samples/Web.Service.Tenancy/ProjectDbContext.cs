using Microsoft.EntityFrameworkCore;
using System.Reflection;
using PlatformFramework.Tenancy;
using Finbuckle.MultiTenant;

namespace Web.Service
{
    public class ProjectDbContext : MultiTenantDbContextCore
    {
        public ProjectDbContext(ITenantInfo tenantInfo, DbContextOptions<ProjectDbContext> options)
            : base(tenantInfo, options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.EnableSensitiveDataLogging();

            builder.UseNpgsql(TenantInfo.ConnectionString, assembly => assembly.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

            base.OnConfiguring(builder);
        }
    }
}

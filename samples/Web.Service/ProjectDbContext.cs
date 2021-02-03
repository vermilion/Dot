using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Identity.Context;

namespace Web.Service
{
    public class ProjectDbContext : IdentityDbContextCore
    {
        public ProjectDbContext(
            DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MyEntityConfiguration());
        }
    }
}

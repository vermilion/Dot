using PlatformFramework.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Web.Service
{
    public class ProjectDbContext : DbContextCore
    {
        public ProjectDbContext(
            DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }
    }
}

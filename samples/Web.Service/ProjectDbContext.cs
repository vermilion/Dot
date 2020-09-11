using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Abstractions;
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
    }
}

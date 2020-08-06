using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PlatformFramework.EFCore.Entities;

namespace PlatformFramework.EFCore
{
    public static class DbContextExtensions
    {
        public static void RegisterPlatformEntities(this ModelBuilder modelBuilder, DbContext context)
        {
            var registry = context.GetService<EntitiesRegistry>();
            registry.Configure(modelBuilder);
        }
    }
}
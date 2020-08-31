using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PlatformFramework.EFCore.Entities;

namespace PlatformFramework.EFCore
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Allows to apply all entity configurations registered
        /// </summary>
        /// <param name="modelBuilder">Builder <see cref="ModelBuilder"/></param>
        /// <param name="context">Database context <see cref="DbContext"/></param>
        public static void ApplyEntitiesConfiguration(this ModelBuilder modelBuilder, DbContext context)
        {
            var registry = context.GetService<EntitiesRegistry>();
            registry.Apply(modelBuilder);
        }
    }
}
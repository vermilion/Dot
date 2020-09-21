using Microsoft.EntityFrameworkCore;

namespace PlatformFramework.EFCore.Entities
{
    public class EfCoreEntitiesRegistryBuilder
    {
        private EntitiesRegistry Registry { get; }

        public EfCoreEntitiesRegistryBuilder(EntitiesRegistry registry)
        {
            Registry = registry;
        }

        public EfCoreEntitiesRegistryBuilder ApplyConfiguration<TEntity, TConfiguration>()
            where TEntity : class
            where TConfiguration : IEntityTypeConfiguration<TEntity>, new()
        {
            var configuration = new TConfiguration();

            Registry.ApplyConfiguration<TEntity, TConfiguration>(configuration);

            return this;
        }
    }
}
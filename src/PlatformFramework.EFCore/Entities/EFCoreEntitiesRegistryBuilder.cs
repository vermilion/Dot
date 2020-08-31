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

        public EfCoreEntitiesRegistryBuilder ApplyConfiguration<TEntity, TCustomizer>()
            where TEntity : class
            where TCustomizer : IEntityTypeConfiguration<TEntity>, new()
        {
            var customizer = new TCustomizer();

            Registry.ApplyConfiguration<TEntity, TCustomizer>(customizer);

            return this;
        }
    }
}
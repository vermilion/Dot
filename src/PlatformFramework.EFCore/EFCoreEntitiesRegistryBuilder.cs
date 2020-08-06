using PlatformFramework.EFCore.Entities;
using PlatformFramework.EFCore.Entities.Customizers;

namespace PlatformFramework.EFCore
{
    public class EfCoreEntitiesRegistryBuilder
    {
        private EntitiesRegistry Registry { get; }

        public EfCoreEntitiesRegistryBuilder(EntitiesRegistry registry)
        {
            Registry = registry;
        }

        public EfCoreEntitiesRegistryBuilder RegisterEntity<TEntity, TCustomizer>(bool addToContext = true)
            where TEntity : class
            where TCustomizer : EntityCustomizer<TEntity>, new()
        {
            var customizer = new TCustomizer
            {
                AddToContext = addToContext
            };

            Registry.RegisterCustomizer<TEntity, TCustomizer>(customizer);

            return this;
        }
    }
}
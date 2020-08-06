using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlatformFramework.EFCore.Entities.Customizers
{
    /// <summary>
    /// Кастомайзер для сущности
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public abstract class EntityCustomizer<TEntity> : EntityCustomizer
        where TEntity : class
    {
        internal bool AddToContext { get; set; } = true;

        public virtual void Configure(EntitiesRegistryBuilder<TEntity> builder)
        {
        }

        /// <summary>
        /// Настройки сущности
        /// </summary>
        public abstract void CustomizeEntity(EntityTypeBuilder<TEntity> entity);

        /// <summary>
        /// Настройки расширения сущности
        /// </summary>
        public virtual void CustomizeEntityExtension(EntityExtensionBuilder<TEntity> extension)
        {
        }

        public sealed override void Customize(ModelBuilder modelBuilder, EntitiesRegistry entitiesRegistry)
        {
            if (!AddToContext)
                return;

            CustomizeEntity(modelBuilder.Entity<TEntity>());

            var extension = new EntityExtensionBuilder<TEntity>(this);
            CustomizeEntityExtension(extension);

            extension.Apply(modelBuilder.Entity<TEntity>());
        }

        public sealed override void Configure(EntitiesRegistry entitiesRegistry)
        {
            var registryBuilder = new EntitiesRegistryBuilder<TEntity>(entitiesRegistry);
            Configure(registryBuilder);
        }
    }
}

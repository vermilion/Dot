using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Context;

namespace PlatformFramework.EFCore.Entities.Customizers
{
    /// <summary>
    /// Базовый кастомайзер для сущности
    /// </summary>
    public abstract class EntityCustomizer
    {
        /// <summary>
        /// Флаги сущности
        /// </summary>
        public EntityConfigFlags Flags { get; set; } = new EntityConfigFlags();

        public abstract void Configure(EntitiesRegistry entitiesRegistry);

        public abstract void Customize(ModelBuilder modelBuilder, EntitiesRegistry entitiesRegistry);
    }
}

using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Context;

namespace PlatformFramework.EFCore.Entities.Customizers
{
    /// <summary>
    /// Default Entities Customizer
    /// </summary>
    public abstract class EntityCustomizer
    {
        /// <summary>
        /// Entity flags
        /// </summary>
        public EntityConfigFlags Flags { get; set; } = new EntityConfigFlags();

        public abstract void Configure(EntitiesRegistry entitiesRegistry);

        public abstract void Customize(ModelBuilder modelBuilder, EntitiesRegistry entitiesRegistry);
    }
}

using Microsoft.EntityFrameworkCore;

namespace PlatformFramework.EFCore.Entities
{
    /// <summary>
    /// Registry
    /// </summary>
    public class EntitiesRegistry : FluentBuilder<ModelBuilder>
    {
        public void ApplyConfiguration<TEntity, TEntityCustomizer>(TEntityCustomizer customizerInstance)
            where TEntity : class
            where TEntityCustomizer : IEntityTypeConfiguration<TEntity>
        {
            AddAction(modelBuilder => modelBuilder.ApplyConfiguration(customizerInstance));
        }
    }
}

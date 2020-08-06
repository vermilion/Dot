using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.Domain.Domain;

namespace PlatformFramework.EFCore.Entities.Customizers
{
    public sealed class EmptyEntityCustomizer<TEntity> : EntityCustomizer<TEntity>
        where TEntity : Entity
    {
        public override void CustomizeEntity(EntityTypeBuilder<TEntity> entity)
        {
        }
    }
}

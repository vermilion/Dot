using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.EFCore.Entities;
using PlatformFramework.EFCore.Entities.Customizers;
using Web.Service.BusinessLogic;

namespace Web.Service
{
    public class MyEntityCustomizer : EntityCustomizer<MyEntity>
    {
        public override void Configure(EntitiesRegistryBuilder<MyEntity> builder)
        {
            builder.MapToDto<MyEntityModel>(x =>
            {
                x.ForMember(m => m.Title, o => o.MapFrom(oo => oo.Title));
            });

            builder.MapFromDto<MyEntityModel>();

            builder
                .MapFromDto<CreateRequest>();
        }

        public override void CustomizeEntity(EntityTypeBuilder<MyEntity> entity)
        {
            entity.ToTable("TableTest");

            entity.Property(x => x.Title)
                .HasColumnName("NewTitle");
        }

        public override void CustomizeEntityExtension(EntityExtensionBuilder<MyEntity> extension)
        {
            extension
                .AddConcurrencyCheck()
                .AddCreationTracking()
                .AddModificationTracking()
                .AddDeletionTracking()
                .AddSoftDelete();
        }
    }
}

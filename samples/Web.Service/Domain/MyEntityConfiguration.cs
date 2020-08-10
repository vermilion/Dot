using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Service
{
    public class MyEntityMappingProfile : Profile
    {
        public MyEntityMappingProfile()
        {
            CreateMap<MyEntity, MyEntityModel>()
                .ForMember(m => m.Title, o => o.MapFrom(oo => oo.Title));

            CreateMap<MyEntityModel, MyEntity>();
        }
    }

    public class MyEntityConfiguration : IEntityTypeConfiguration<MyEntity>
    {
        public void Configure(EntityTypeBuilder<MyEntity> entity)
        {
            entity.ToTable("TableTest");

            entity.Property(x => x.Title)
                .HasColumnName("NewTitle");
        }
    }
}

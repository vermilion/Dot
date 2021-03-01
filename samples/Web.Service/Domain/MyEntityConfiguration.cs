using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Service
{
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

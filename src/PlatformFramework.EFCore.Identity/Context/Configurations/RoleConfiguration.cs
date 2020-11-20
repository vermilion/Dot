using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Context.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.HasKey(r => r.Id);
            entity.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();
            entity.ToTable("AspNetRoles");
            entity.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            entity.Property(u => u.Name).HasMaxLength(256);
            entity.Property(u => u.NormalizedName).HasMaxLength(256);

            entity.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
            entity.HasMany(x => x.Claims).WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        }
    }
}

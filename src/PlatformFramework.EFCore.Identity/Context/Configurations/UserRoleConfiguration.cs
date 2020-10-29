using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Context.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(r => new { r.UserId, r.RoleId });
            entity.ToTable("AspNetUserRoles");
        }
    }
}

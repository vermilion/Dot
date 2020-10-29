using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Context.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> entity)
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(l => new { l.LoginProvider, l.ProviderKey }).HasName("UserLoginProviderIndex").IsUnique();
            entity.ToTable("AspNetUserLogins");
        }
    }
}

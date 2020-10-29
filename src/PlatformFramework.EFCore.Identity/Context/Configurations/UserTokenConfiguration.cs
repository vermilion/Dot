using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Context.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> entity)
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(t => new { t.UserId, t.LoginProvider, t.Name }).HasName("UserTokenUserIndex").IsUnique();
            entity.ToTable("AspNetUserTokens");
        }
    }
}

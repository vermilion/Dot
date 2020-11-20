using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            entity.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");
            entity.ToTable("AspNetUsers");
            entity.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            entity.Property(u => u.UserName).HasMaxLength(256);
            entity.Property(u => u.NormalizedUserName).HasMaxLength(256);
            entity.Property(u => u.Email).HasMaxLength(256);
            entity.Property(u => u.NormalizedEmail).HasMaxLength(256);

            entity.HasMany(x => x.Claims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            entity.HasMany(x => x.Logins).WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            entity.HasMany(x => x.Tokens).WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            entity.HasMany(x => x.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}

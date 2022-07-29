using Cofoundry.Core;
using Cofoundry.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cofoundry.Domain.Data
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", DbConstants.DotSchema);

            builder.HasKey(x => x.UserId);

            builder.Property(s => s.FirstName)
                .HasMaxLength(32);

            builder.Property(s => s.LastName)
                .HasMaxLength(32);

            builder.Property(s => s.Email)
                .HasMaxLength(150);

            builder.Property(s => s.Username)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.Password);

            // Relationships

            builder.HasOne(s => s.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId);

            #region create auditable (ish)
            
            builder.HasOne(s => s.Creator)
                .WithMany()
                .HasForeignKey(d => d.CreatorId);

            #endregion
        }
    }
}

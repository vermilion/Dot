using Cofoundry.Core;
using Cofoundry.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cofoundry.Domain.Data
{
    public class UserPasswordResetRequestMap : IEntityTypeConfiguration<UserPasswordResetRequest>
    {
        public void Configure(EntityTypeBuilder<UserPasswordResetRequest> builder)
        {
            builder.ToTable("UserPasswordResetRequest", DbConstants.CofoundrySchema);

            builder.Property(s => s.Token);

            builder.Property(s => s.IPAddress)
                .IsUnicode(false)
                .HasMaxLength(45);

            // Relationships

            builder.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);
        }
    }
}

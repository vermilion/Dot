using Cofoundry.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cofoundry.Domain.Data
{
    public class FailedAuthenticationAttemptMap : IEntityTypeConfiguration<FailedAuthenticationAttempt>
    {
        public void Configure(EntityTypeBuilder<FailedAuthenticationAttempt> builder)
        {
            builder.ToTable("FailedAuthenticationAttempt", DbConstants.DotSchema);

            builder.HasKey(s => s.FailedAuthenticationAttemptId);

            builder.Property(s => s.UserName)
                .IsUnicode(false);

            builder.Property(s => s.IPAddress)
                .IsUnicode(false)
                .HasMaxLength(45);
        }
    }
}

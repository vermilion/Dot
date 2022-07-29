using Cofoundry.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cofoundry.Domain.Data
{
    public class UserLoginLogMap : IEntityTypeConfiguration<UserLoginLog>
    {
        public void Configure(EntityTypeBuilder<UserLoginLog> builder)
        {
            builder.ToTable("UserLoginLog", DbConstants.CofoundrySchema);

            builder.HasKey(s => s.UserLoginLogId);

            builder.Property(s => s.IPAddress)
                .IsUnicode(false)
                .HasMaxLength(45);
        }
    }
}

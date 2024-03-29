using Cofoundry.Core;
using Cofoundry.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Cofoundry.Domain.Data
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", DbConstants.DotSchema);

            builder.Property(s => s.PermissionCode)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(s => s.EntityDefinitionCode)
                .HasMaxLength(6);

            builder.HasOne(s => s.EntityDefinition)
                .WithMany(d => d.Permissions)
                .HasForeignKey(s => s.EntityDefinitionCode);
        }
    }
}

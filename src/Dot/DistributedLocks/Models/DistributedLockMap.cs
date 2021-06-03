using Cofoundry.Core;
using Cofoundry.Core.DistributedLocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cofoundry.Domain.Data
{
    public class DistributedLockMap : IEntityTypeConfiguration<DistributedLockEntity>
    {
        public void Configure(EntityTypeBuilder<DistributedLockEntity> builder)
        {
            builder.ToTable("DistributedLock", DbConstants.CofoundrySchema);

            builder.HasKey(x => x.DistributedLockId);
        }
    }
}

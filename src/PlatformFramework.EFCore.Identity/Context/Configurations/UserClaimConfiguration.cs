using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformFramework.EFCore.Identity.Entities;

namespace PlatformFramework.EFCore.Identity.Context.Configurations
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> entity)
        {
            entity.HasKey(uc => uc.Id);
            entity.ToTable("AspNetUserClaims");
        }
    }
}

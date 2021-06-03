using Cofoundry.Core.DistributedLocks;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Abstractions;

namespace Cofoundry.Domain.Data
{
    public static class UnitOfWorkExtensions
    {
        public static DbSet<DistributedLockEntity> DistributedLocks(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<DistributedLockEntity>();
        }
    }
}

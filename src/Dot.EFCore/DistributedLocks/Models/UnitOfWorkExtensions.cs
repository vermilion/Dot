using Cofoundry.Core.DistributedLocks;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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

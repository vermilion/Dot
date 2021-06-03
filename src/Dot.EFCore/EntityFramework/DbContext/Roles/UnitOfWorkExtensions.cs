using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Data
{
    public static class RoleUnitOfWorkExtensions
    {
        public static DbSet<Role> Roles(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<Role>();
        }
    }
}

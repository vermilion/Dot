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

        public static DbSet<Permission> Permissions(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<Permission>();
        }

        public static DbSet<RolePermission> RolePermissions(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<RolePermission>();
        }
    }
}

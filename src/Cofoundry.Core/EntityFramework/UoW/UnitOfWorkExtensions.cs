using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace PlatformFramework.EFCore.Abstractions
{
    public static class UnitOfWorkExtensions
    {
        public static DbSet<User> Users(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<User>();
        }
    }
}
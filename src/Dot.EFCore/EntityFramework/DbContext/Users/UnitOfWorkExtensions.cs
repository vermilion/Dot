using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Data
{
    public static class UserUnitOfWorkExtensions
    {
        public static DbSet<FailedAuthenticationAttempt> FailedAuthenticationAttempts(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<FailedAuthenticationAttempt>();
        }

        public static DbSet<User> Users(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<User>();
        }

        public static DbSet<UserPasswordResetRequest> UserPasswordResetRequests(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<UserPasswordResetRequest>();
        }
    }
}

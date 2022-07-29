using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Data
{
    /// <summary>
    /// Extend DbModelBuilder to reduce boilerplate code when
    /// setting up a DbContext for a Cofoundry implementation.
    /// </summary>
    public static class DbModelBuilderExtensions
    {
        /// <summary>
        /// Maps Cofoundry Users classes to the DbModelBuilder
        /// </summary>
        /// <returns>DbModelBuilder for method chaining</returns>
        public static ModelBuilder MapCofoundryUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new UserMap())
                .ApplyConfiguration(new RoleMap())
                .ApplyConfiguration(new PermissionMap())
                .ApplyConfiguration(new RolePermissionMap())
                .ApplyConfiguration(new UserPasswordResetRequestMap())
                .ApplyConfiguration(new FailedAuthenticationAttemptMap())
                .ApplyConfiguration(new UserLoginLogMap()); ;

            return modelBuilder;
        }

        public static ModelBuilder MapCustomEntities(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new EntityDefinitionMap());

            return modelBuilder;
        }
    }
}

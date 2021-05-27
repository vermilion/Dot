using Cofoundry.Core;
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
        /// <para>
        /// Makes the "app" schema as the default. "app" is the recommended
        /// schema to use for any custom tables you add to the database, keeping
        /// them separate from Cofoundry tables and any other tables created
        /// by 3rd parties.
        /// </para>
        /// <para>
        /// This schema can also be references with DbConstants.DefaultAppSchema
        /// </para>
        /// </summary>
        /// <returns>DbModelBuilder for method chaining.</returns>
        public static ModelBuilder HasAppSchema(this ModelBuilder modelBuilder)
        {
            return modelBuilder.HasDefaultSchema(DbConstants.DefaultAppSchema);
        }

        #region common Cofoundry object mapping

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
                ;

            return modelBuilder;
        }

        /// <summary>
        /// Maps Cofoundry page, custom entities, images and all dependency classes 
        /// to the DbModelBuilder.
        /// </summary>
        /// <returns>DbModelBuilder for method chaining</returns>
        public static ModelBuilder MapCofoundryContent(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .MapCofoundryUsers()
                .ApplyConfiguration(new EntityDefinitionMap());

            return modelBuilder;
        }

        #endregion
    }
}

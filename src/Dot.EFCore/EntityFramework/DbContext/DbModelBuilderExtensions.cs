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
    }
}

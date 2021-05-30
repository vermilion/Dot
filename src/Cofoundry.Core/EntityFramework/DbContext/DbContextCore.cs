using Cofoundry.Core;
using Cofoundry.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace Cofoundry.Domain.Data
{
    /// <summary>
    /// The main Cofoundry entity framework DbContext representing all the main 
    /// entities in the Cofoundry database. Direct access to the DbContext is
    /// discouraged, instead we advise you use the domain queries and commands
    /// available in the Cofoundry data repositories, see
    /// https://github.com/cofoundry-cms/cofoundry/wiki/Data-Access#repositories
    /// </summary>
    public class DbContextCore : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ICofoundryDbConnectionManager _cofoundryDbConnectionFactory;
        private readonly DatabaseSettings _databaseSettings;

        public DbContextCore(
            ILoggerFactory loggerFactory,
            ICofoundryDbConnectionManager cofoundryDbConnectionFactory,
            DatabaseSettings databaseSettings)
        {
            _loggerFactory = loggerFactory;
            _cofoundryDbConnectionFactory = cofoundryDbConnectionFactory;
            _databaseSettings = databaseSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            var connection = _cofoundryDbConnectionFactory.GetShared();
            ConfigureAction(optionsBuilder, connection);
        }

        public virtual void ConfigureAction(DbContextOptionsBuilder builder, DbConnection connection)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema(DbConstants.CofoundrySchema)
                .MapCofoundryContent()
                .ApplyConfiguration(new SettingMap());
        }

        #region properties

        public DbSet<EntityDefinition> EntityDefinitions { get; set; }

        /// <summary>
        /// A permission represents an type action a user can
        /// be permitted to perform. Typically this is associated
        /// with a specified entity type, but doesn't have to be e.g.
        /// "read pages", "access dashboard", "delete images". The 
        /// combination of EntityDefinitionCode and PermissionCode
        /// must be unique
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Roles are an assignable collection of permissions. Every user has to 
        /// be assigned to one role.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Setting> Settings { get; set; }

        /// <summary>
        /// Represents the user in the Cofoundry custom identity system
        /// </summary>
        public DbSet<User> Users { get; set; }

        #endregion
    }
}

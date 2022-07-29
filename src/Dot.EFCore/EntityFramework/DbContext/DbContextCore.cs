using Cofoundry.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        protected DatabaseSettings DatabaseSettings { get; }

        public DbContextCore(
            ILoggerFactory loggerFactory,
            DatabaseSettings databaseSettings)
        {
            _loggerFactory = loggerFactory;
            DatabaseSettings = databaseSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            ConfigureAction(optionsBuilder);
        }

        public virtual void ConfigureAction(DbContextOptionsBuilder builder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema(DbConstants.CofoundrySchema)
                .ApplyConfiguration(new SettingMap());
        }

        #region properties

        public DbSet<Setting> Settings { get; set; }

        #endregion
    }
}

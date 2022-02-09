using Cofoundry.Core;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cofoundry.BasicTestSite
{
    public class AppDbContext : DbContextCore
    {
        public AppDbContext(
            ILoggerFactory loggerFactory,
            DatabaseSettings databaseSettings)
            : base(loggerFactory, databaseSettings)
        {
        }

        public override void ConfigureAction(DbContextOptionsBuilder builder)
        {
            var connectionString = DatabaseSettings.ConnectionString;
            builder.UseNpgsql(connectionString);
        }
    }
}

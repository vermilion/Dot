using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Extensions.Logging;
using Cofoundry.Core.Data;
using Cofoundry.Core;
using Cofoundry.Domain.Data;

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

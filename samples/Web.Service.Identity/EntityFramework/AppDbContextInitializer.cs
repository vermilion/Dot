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
            ICofoundryDbConnectionManager cofoundryDbConnectionFactory,
            DatabaseSettings databaseSettings)
            : base(loggerFactory, cofoundryDbConnectionFactory, databaseSettings)
        {
        }

        public override void ConfigureAction(DbContextOptionsBuilder builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

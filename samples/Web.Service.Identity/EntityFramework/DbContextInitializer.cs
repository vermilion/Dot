using System;
using Cofoundry.Core.EntityFramework.Internal;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Extensions.Logging;
using Cofoundry.Core.Data;
using Cofoundry.Core;

namespace Cofoundry.BasicTestSite
{
    public class DbContextInitializer : CofoundryDbContextInitializer
    {
        public DbContextInitializer(
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

using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Core;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Updates user auditing information in the database to record 
    /// the successful login. Does not do anything to login a user
    /// session.
    /// </summary>
    public class LogSuccessfulLoginCommandHandler 
        : IRequestHandler<LogSuccessfulLoginCommand, Unit>
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IClientConnectionService _clientConnectionService;

        public LogSuccessfulLoginCommandHandler(
            CofoundryDbContext dbContext,
            IClientConnectionService clientConnectionService
            )
        {
            _dbContext = dbContext;
            _clientConnectionService = clientConnectionService;
        }

        #endregion

        public async Task<Unit> ExecuteAsync(LogSuccessfulLoginCommand command, IExecutionContext executionContext)
        {
            var user = await Query(command.UserId).SingleOrDefaultAsync();
            EntityNotFoundException.ThrowIfNull(user, command.UserId);

            var connectionInfo = _clientConnectionService.GetConnectionInfo();
            SetLoggedIn(user, executionContext);
            await _dbContext.SaveChangesAsync();

            /*await _sqlExecutor.ExecuteCommandAsync(_dbContext,
                "Cofoundry.UserLoginLog_Add",
                new SqlParameter("UserId", user.UserId),
                new SqlParameter("IPAddress", connectionInfo.IPAddress),
                new SqlParameter("DateTimeNow", executionContext.ExecutionDate)
                );*/

            return Unit.Value;
        }

        private IQueryable<User> Query(int userId)
        {
            var user = _dbContext
                .Users
                .FilterById(userId)
                .FilterCanLogIn();

            return user;
        }

        private void SetLoggedIn(User user, IExecutionContext executionContext)
        {
            user.PreviousLoginDate = user.LastLoginDate;
            user.LastLoginDate = executionContext.ExecutionDate;
        }
    }
}

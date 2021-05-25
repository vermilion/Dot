using System.Threading.Tasks;
using Cofoundry.Core;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain.Internal
{
    public class LogFailedLoginAttemptCommandHandler
        : IRequestHandler<LogFailedLoginAttemptCommand, Unit>
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IClientConnectionService _clientConnectionService;

        public LogFailedLoginAttemptCommandHandler(
            CofoundryDbContext dbContext,
            IClientConnectionService clientConnectionService
            )
        {
            _dbContext = dbContext;
            _clientConnectionService = clientConnectionService;
        }
        #endregion

        public async Task<Unit> ExecuteAsync(LogFailedLoginAttemptCommand command, IExecutionContext executionContext)
        {
            var connectionInfo = _clientConnectionService.GetConnectionInfo();

            await _dbContext.FailedAuthenticationAttempts.AddAsync(new FailedAuthenticationAttempt
            {
                UserName = TextFormatter.Limit(command.Username, 150),
                IPAddress = connectionInfo.IPAddress,
                AttemptDate = executionContext.ExecutionDate
            });

            return Unit.Value;
        }
    }
}

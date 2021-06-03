using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;

namespace Cofoundry.Domain.Internal
{
    public class LogFailedLoginAttemptCommandHandler
        : IRequestHandler<LogFailedLoginAttemptCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        #region constructor

        private readonly IClientConnectionService _clientConnectionService;

        public LogFailedLoginAttemptCommandHandler(
            IUnitOfWork unitOfWork,
            IClientConnectionService clientConnectionService
            )
        {
            _unitOfWork = unitOfWork;
            _clientConnectionService = clientConnectionService;
        }
        #endregion

        public async Task<Unit> ExecuteAsync(LogFailedLoginAttemptCommand command, IExecutionContext executionContext)
        {
            var connectionInfo = _clientConnectionService.GetConnectionInfo();

            await _unitOfWork.FailedAuthenticationAttempts().AddAsync(new FailedAuthenticationAttempt
            {
                UserName = command.Username,
                IPAddress = connectionInfo.IPAddress,
                AttemptDate = executionContext.ExecutionDate
            });

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}

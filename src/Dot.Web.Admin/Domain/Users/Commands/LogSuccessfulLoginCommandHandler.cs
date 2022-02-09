using Cofoundry.Core;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientConnectionService _clientConnectionService;

        public LogSuccessfulLoginCommandHandler(
            IUnitOfWork unitOfWork,
            IClientConnectionService clientConnectionService)
        {
            _unitOfWork = unitOfWork;
            _clientConnectionService = clientConnectionService;
        }

        public async Task<Unit> ExecuteAsync(LogSuccessfulLoginCommand command, IExecutionContext executionContext)
        {
            var user = _unitOfWork.Users()
                .WithSpecification(new UserByIdSpec(command.UserId))
                .SingleOrDefault();

            EntityNotFoundException.ThrowIfNull(user, command.UserId);

            var connectionInfo = _clientConnectionService.GetConnectionInfo();
            SetLoggedIn(user, executionContext);

            await _unitOfWork.UserLoginLogs().AddAsync(new UserLoginLog
            {
                UserId = command.UserId,
                IPAddress = connectionInfo.IPAddress,
                AttemptDate = executionContext.ExecutionDate
            });

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }

        private void SetLoggedIn(User user, IExecutionContext executionContext)
        {
            user.PreviousLoginDate = user.LastLoginDate;
            user.LastLoginDate = executionContext.ExecutionDate;
        }
    }
}

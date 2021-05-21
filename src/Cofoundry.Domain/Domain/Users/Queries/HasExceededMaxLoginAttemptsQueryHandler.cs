using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain.Internal
{
    public class HasExceededMaxLoginAttemptsQueryHandler 
        : IQueryHandler<HasExceededMaxLoginAttemptsQuery, bool>
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IClientConnectionService _clientConnectionService;

        public HasExceededMaxLoginAttemptsQueryHandler(
            CofoundryDbContext dbContext,
            AuthenticationSettings authenticationSettings,
            IClientConnectionService clientConnectionService
            )
        {
            _dbContext = dbContext;
            _authenticationSettings = authenticationSettings;
            _clientConnectionService = clientConnectionService;
        }
        
        #endregion

        #region execution

        public async Task<bool> ExecuteAsync(HasExceededMaxLoginAttemptsQuery query, IExecutionContext executionContext)
        {
            var connectionInfo = _clientConnectionService.GetConnectionInfo();

            /*var isValid = await _sqlExecutor.ExecuteScalarAsync<int>(_dbContext,
                "Cofoundry.FailedAuthticationAttempt_IsAttemptValid",
                new SqlParameter("UserAreaCode", query.UserAreaCode),
                new SqlParameter("Username", query.Username.Trim()),
                new SqlParameter("IPAddress", connectionInfo.IPAddress),
                new SqlParameter("DateTimeNow", executionContext.ExecutionDate),
                new SqlParameter("MaxIPAttempts", _authenticationSettings.MaxIPAttempts),
                new SqlParameter("MaxUsernameAttempts", _authenticationSettings.MaxUsernameAttempts),
                new SqlParameter("MaxIPAttemptsBoundaryInMinutes", _authenticationSettings.MaxIPAttemptsBoundaryInMinutes),
                new SqlParameter("MaxUsernameAttemptsBoundaryInMinutes", _authenticationSettings.MaxUsernameAttemptsBoundaryInMinutes)
                );

            return isValid != 1;*/
            return false;
        }

        #endregion
    }

}

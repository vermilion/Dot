using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.Transactions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    public class MarkAsSetUpCommandHandler 
        : IRequestHandler<MarkAsSetUpCommand, Unit>
    {
        private const string SETTING_KEY = "IsSetup";

        #region constructor

        private readonly DbContextCore _dbContext;
        private readonly ISettingCache _settingCache;
        private readonly IPermissionValidationService _permissionValidationService;
        private readonly ITransactionScopeManager _transactionScopeFactory;

        public MarkAsSetUpCommandHandler(
            DbContextCore dbContext,
            SettingCommandHelper settingCommandHelper,
            ISettingCache settingCache,
            IPermissionValidationService permissionValidationService,
            ITransactionScopeManager transactionScopeFactory
            )
        {
            _dbContext = dbContext;
            _settingCache = settingCache;
            _permissionValidationService = permissionValidationService;
            _transactionScopeFactory = transactionScopeFactory;
        }

        #endregion

        #region execute

        public async Task<Unit> ExecuteAsync(MarkAsSetUpCommand command, IExecutionContext executionContext)
        {
            _permissionValidationService.EnforceIsSuperAdminRole(executionContext.UserContext);

            var setting = await _dbContext
                .Settings
                .SingleOrDefaultAsync(s => s.SettingKey == SETTING_KEY);

            if (setting == null)
            {
                setting = new Setting
                {
                    SettingKey = SETTING_KEY,
                    CreateDate = executionContext.ExecutionDate,
                    UpdateDate = executionContext.ExecutionDate
                };

                _dbContext.Settings.Add(setting);
            }

            setting.SettingValue = "true";

            await _dbContext.SaveChangesAsync();
            await _transactionScopeFactory.QueueCompletionTaskAsync(_dbContext, () => Task.Run(_settingCache.Clear));

            return Unit.Value;
        }

        #endregion
    }
}

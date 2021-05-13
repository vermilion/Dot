using Cofoundry.Core.Data;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    public class MarkAsSetUpCommandHandler 
        : ICommandHandler<MarkAsSetUpCommand>
        , IIgnorePermissionCheckHandler
    {
        private const string SETTING_KEY = "IsSetup";

        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly ISettingCache _settingCache;
        private readonly IPermissionValidationService _permissionValidationService;
        private readonly ITransactionScopeManager _transactionScopeFactory;

        public MarkAsSetUpCommandHandler(
            CofoundryDbContext dbContext,
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

        public async Task ExecuteAsync(MarkAsSetUpCommand command, IExecutionContext executionContext)
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
            _transactionScopeFactory.QueueCompletionTask(_dbContext, _settingCache.Clear);
        }

        #endregion
    }
}

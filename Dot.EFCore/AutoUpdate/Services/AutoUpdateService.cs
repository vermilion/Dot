using Cofoundry.Core.AutoUpdate;
using Cofoundry.Core.DistributedLocks;
using Dot.EFCore.AutoUpdate.Services.Interfaces;
using Dot.EFCore.Health.Services;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Dot.EFCore.AutoUpdate.Services
{
    /// <summary>
    /// Service to update applications and modules. Typically this is
    /// run at application startup.
    /// </summary>
    public class AutoUpdateService : IAutoUpdateService
    {
        #region private variables

        private static readonly MethodInfo _runVersionedCommandMethod = typeof(AutoUpdateService).GetMethod(nameof(ExecuteGenericVersionedCommand), BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo _runAlwaysRunCommandMethod = typeof(AutoUpdateService).GetMethod(nameof(ExecuteGenericAlwaysRunCommand), BindingFlags.NonPublic | BindingFlags.Instance);
        private readonly ILogger<AutoUpdateService> _logger;
        private readonly IUpdateCommandHandlerFactory _commandHandlerFactory;
        private readonly DbContext _db;
        private readonly IDbHealthChecker _healthChecker;
        private readonly AutoUpdateSettings _autoUpdateSettings;
        private readonly IDistributedLockManager _distributedLockManager;

        #endregion

        #region constructor

        public AutoUpdateService(
            ILogger<AutoUpdateService> logger,
            IUpdateCommandHandlerFactory commandHandlerFactory,
            IUnitOfWork unitOfWork,
            IDbHealthChecker healthChecker,
            AutoUpdateSettings autoUpdateSettings,
            IDistributedLockManager distributedLockManager
            )
        {
            _logger = logger;
            _commandHandlerFactory = commandHandlerFactory;
            _db = unitOfWork.Context;
            _healthChecker = healthChecker;
            _autoUpdateSettings = autoUpdateSettings;
            _distributedLockManager = distributedLockManager;
        }

        #endregion

        #region update

        /// <summary>
        /// Updates an application and referenced modules by scanning for implementations
        /// of IUpdatePackageFactory and executing any packages found.
        /// </summary>
        public async Task UpdateAsync(CancellationToken cancellationToken = default)
        {
            var lockKey = "AUTO_UPDATE_PROCESS";

            var isLocked = await _distributedLockManager.IsLockedAsync(lockKey);

            if (isLocked)
                throw new AutoUpdateProcessLockedException();

            if (IsCancelled(cancellationToken)) return;

            var timeout = TimeSpan.FromSeconds(_autoUpdateSettings.ProcessLockTimeoutInSeconds);

            // Lock the process to prevent concurrent updates
            var distributedLock = await _distributedLockManager.LockAsync(lockKey, timeout);

            try
            {
                if (IsCancelled(cancellationToken))
                    return;

                _logger.LogInformation($"Migrating database associated with context {_db.GetType().Name}");

                await _healthChecker.TestConnection(_db, cancellationToken);

                await _db.Database.MigrateAsync(cancellationToken);

                //if (setup != null)
                //    await setup.Seed(cancellationToken);

                _logger.LogInformation($"Migrated database associated with context {_db.GetType().Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while migrating the database used on context {_db.GetType().Name}");
                throw;
            }
            finally
            {
                await _distributedLockManager.UnlockAsync(distributedLock);
            }
        }

        /*
         * private IEnumerable<IAlwaysRunUpdateCommand> GetAlwaysUpdateCommand()
        {
            yield return new RegisterPermissionsAndRolesUpdateCommand();
        }

        private IEnumerable<IVersionedUpdateCommand> GetAdditionalCommands(ModuleVersion moduleVersion)
        {
            var importPermissionsCommand = new ImportPermissionsCommand();
            if (moduleVersion == null || moduleVersion.Version < importPermissionsCommand.Version)
            {
                yield return new ImportPermissionsCommand();
            }
        }
         */

        private static bool IsCancelled(CancellationToken? cancellationToken)
        {
            return cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested;
        }

        private Task ExecuteGenericVersionedCommand<TCommand>(TCommand command) where TCommand : IVersionedUpdateCommand
        {
            var runner = _commandHandlerFactory.CreateVersionedCommand<TCommand>();
            return runner.ExecuteAsync(command);
        }

        private Task ExecuteGenericAlwaysRunCommand<TCommand>(TCommand command) where TCommand : IAlwaysRunUpdateCommand
        {
            var runner = _commandHandlerFactory.CreateAlwaysRunCommand<TCommand>();
            return runner.ExecuteAsync(command);
        }

        #endregion
    }
}

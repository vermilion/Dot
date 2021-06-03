using Cofoundry.Core.AutoUpdate;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly IAutoUpdateDistributedLockManager _autoUpdateDistributedLockManager;

        #endregion

        #region constructor

        public AutoUpdateService(
            ILogger<AutoUpdateService> logger,
            IUpdateCommandHandlerFactory commandHandlerFactory,
            IUnitOfWork unitOfWork,
            IDbHealthChecker healthChecker,
            AutoUpdateSettings autoUpdateSettings,
            IAutoUpdateDistributedLockManager autoUpdateDistributedLockManager
            )
        {
            _logger = logger;
            _commandHandlerFactory = commandHandlerFactory;
            _db = unitOfWork.Context();
            _healthChecker = healthChecker;
            _autoUpdateSettings = autoUpdateSettings;
            _autoUpdateDistributedLockManager = autoUpdateDistributedLockManager;
        }

        #endregion

        #region update

        /// <summary>
        /// Updates an application and referenced modules by scanning for implementations
        /// of IUpdatePackageFactory and executing any packages found.
        /// </summary>
        public async Task UpdateAsync(CancellationToken cancellationToken = default)
        {
            var isLocked = await IsLockedAsync();

            if (isLocked)
                throw new DatabaseLockedException();

            if (IsCancelled(cancellationToken)) return;

            // TODO: avoid lock when no DB exist
            // Lock the process to prevent concurrent updates
            //var distributedLock = await _autoUpdateDistributedLockManager.LockAsync();

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
                //await _autoUpdateDistributedLockManager.UnlockAsync(distributedLock);
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

        #region locking

        /// <summary>
        /// Works out whether the database is locked for 
        /// schema updates. This is different to distributed locking which 
        /// is intended to prevent multile update instances running.
        /// </summary>
        public async Task<bool> IsLockedAsync()
        {
            /*
            // First check config
            if (_autoUpdateSettings.Disabled) return true;

            // else this option can also be set in the db
            var query = @"
                if (exists (select * 
                                 from information_schema.tables 
                                 where table_schema = 'Cofoundry' 
                                 and  table_name = 'AutoUpdateLock'))
                begin
                    select IsLocked from Cofoundry.AutoUpdateLock;
                end";

            var isLocked = await _db.ReadAsync(query, (r) =>
            {
                return (bool)r["IsLocked"];
            });
            */
            return false;
        }

        /// <summary>
        /// Sets a flag in the database to enable/disable database updates.
        /// </summary>
        /// <param name="isLocked">True to lock the database and prevent schema updates</param>
        public Task SetLockedAsync(bool isLocked)
        {
            //var cmd = "update Cofoundry.AutoUpdateLock set IsLocked = @IsLocked";
            //return _db.ExecuteAsync(cmd, new SqlParameter("@IsLocked", isLocked));

            return Task.CompletedTask;
        }

        #endregion
    }
}

using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Core.DistributedLocks.Internal
{
    /// <summary>
    /// A distributed lock is a locking mechanism that can be used to
    /// prevents code from running in multiple concurrent processes, particularly 
    /// in scenarios where processes might be running in more than one applicatin
    /// or application instance. A central database is used to hold the locking
    /// information.
    /// </summary>
    /// <remarks>
    /// The distributed lock manager is used by the auto-update 
    /// process and will run before any db updates have been made, 
    /// therefore SQL has to be defined in code instead of stored
    /// proces. The auto update process created the initial db
    /// infrastructure for the distributed lock manager.
    /// </remarks>
    public class DistributedLockManager : IDistributedLockManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedLockDefinitionRepository _distributedLockDefinitionRepository;

        public DistributedLockManager(
            IUnitOfWork unitOfWork,
            IDistributedLockDefinitionRepository distributedLockDefinitionRepository
            )
        {
            _unitOfWork = unitOfWork;
            _distributedLockDefinitionRepository = distributedLockDefinitionRepository;
        }

        /// <summary>
        /// Creates a lock for the specified definition using a unique
        /// lock id that represents the running process. You can
        /// query the returned DistributedLock instance to determine if 
        /// the lock was successful.
        /// </summary>
        /// <typeparam name="TDefinition">
        /// The definition type that conmtains the locking parameters that
        /// represent the process to be run.
        /// </typeparam>
        /// <returns>
        /// Returns the current state of the lock for the specified 
        /// definition. If the lock could not be made then the state will
        /// contain information about the existing lock, if the lock was 
        /// successful then the new lock will be returned. You can
        /// query the returned object to determine if the lock was successful.
        /// </returns>
        public async Task<DistributedLock> LockAsync<TDefinition>()
            where TDefinition : IDistributedLockDefinition
        {
            var distributedLockDefinition = _distributedLockDefinitionRepository.Get<TDefinition>();
            EntityNotFoundException.ThrowIfNull(distributedLockDefinition, typeof(TDefinition));

            var lockingId = Guid.NewGuid();
            var now = DateTime.Now;

            var distributedLock = await _unitOfWork.DistributedLocks()
                .Where(x => x.DistributedLockId == distributedLockDefinition.DistributedLockId)
                .SingleOrDefaultAsync();

            if (distributedLock == null)
            {
                distributedLock = new DistributedLockEntity
                {
                    DistributedLockId = distributedLockDefinition.DistributedLockId,
                    Name = distributedLockDefinition.Name
                };

                await _unitOfWork.DistributedLocks().AddAsync(distributedLock);
            }

            if (distributedLock.ExpiryDate.GetValueOrDefault(DateTime.MinValue) < now)
            {
                distributedLock.LockingId = lockingId;
                distributedLock.LockDate = now;
                distributedLock.ExpiryDate = now.AddSeconds(distributedLockDefinition.Timeout.TotalSeconds);
            }

            await _unitOfWork.SaveChangesAsync();

            return new DistributedLock(distributedLock) { RequestedLockingId = lockingId };

            /*var query = @" 
                declare @DateNow datetime2(7) = GetUtcDate();

                with data as (select @DistributedLockId as DistributedLockId, @DistributedLockName as [Name])
                merge Cofoundry.DistributedLock t
                using data s on s.DistributedLockId = t.DistributedLockId
                when not matched by target
                then insert (DistributedLockId, [Name]) 
                values (s.DistributedLockId, s.[Name]);

                update Cofoundry.DistributedLock 
                set LockingId = @LockingId, LockDate = @DateNow, ExpiryDate = dateadd(second, @TimeoutInSeconds, @DateNow)
                where DistributedLockId = @DistributedLockId
                and (LockingId is null or ExpiryDate < @DateNow)

                select DistributedLockId, LockingId, LockDate, ExpiryDate 
                from Cofoundry.DistributedLock
                where DistributedLockId = @DistributedLockId
                ";

            var distributedLock = (await _db.ReadAsync(query,
                MapDistributedLock,
                new SqlParameter("DistributedLockId", distributedLockDefinition.DistributedLockId),
                new SqlParameter("DistributedLockName", distributedLockDefinition.Name),
                new SqlParameter("LockingId", lockingId),
                new SqlParameter("TimeoutInSeconds", distributedLockDefinition.Timeout.TotalSeconds)
                ))
                .SingleOrDefault();

            if (distributedLock == null)
            {
                throw new Exception($"Unknown error creating a distributed lock with a DistributedLockId of '{distributedLockDefinition.DistributedLockId}'");
            }
            */
        }

        /// <summary>
        /// Unlocks the specified distributed lock, freeing it up
        /// for other processes to use.
        /// </summary>
        /// <param name="distributedLock">
        /// The distributed lock entry to unlock. This should be the instance
        /// you received from a call to the LockAsync method.
        /// </param>
        public async Task UnlockAsync(DistributedLock distributedLock)
        {
            if (distributedLock == null) throw new ArgumentNullException(nameof(distributedLock));

            var existingLocks = await _unitOfWork.DistributedLocks()
                .Where(x => x.LockingId == distributedLock.Entity.LockingId && x.DistributedLockId == distributedLock.Entity.DistributedLockId)
                .ToListAsync();

            _unitOfWork.DistributedLocks().RemoveRange(existingLocks);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Interfaces.Threading
{
    /// <summary>
    /// Simple background queue
    /// </summary>
    public interface IBackgroundTaskQueue
    {
        /// <summary>
        /// Queue item
        /// </summary>
        /// <param name="workItem"></param>
        void QueueBackgroundWorkItem(Func<CancellationToken, IServiceProvider, Task> workItem);

        /// <summary>
        /// Dequeue item
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Func<CancellationToken, IServiceProvider, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
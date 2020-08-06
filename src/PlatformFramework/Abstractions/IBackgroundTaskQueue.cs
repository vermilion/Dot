using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Interfaces.Threading
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, IServiceProvider, Task> workItem);

        Task<Func<CancellationToken, IServiceProvider, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
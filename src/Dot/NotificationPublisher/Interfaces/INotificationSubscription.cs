using System;
using System.Threading.Tasks;

namespace Cofoundry.Core.MessageAggregator
{
    public interface INotificationSubscription
    {
        bool CanDeliver<TMessage>();

        Task DeliverAsync(IServiceProvider serviceProvider, object message);
    }
}

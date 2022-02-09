using System.Collections.Generic;

namespace Cofoundry.Core.MessageAggregator
{
    /// <summary>
    /// State object to keep track of subscriptions to a message aggregator
    /// </summary>
    public interface INotificationPublisherState
    {
        /// <summary>
        /// Gets a collection of subscriptions for the specified message
        /// </summary>
        /// <typeparam name="TMessage">Type of message to get</typeparam>
        IEnumerable<INotificationSubscription> GetSubscriptionsFor<TMessage>();

        /// <summary>
        /// Adds a new message subscription to the state
        /// </summary>
        /// <param name="subscription">the message subscription to add to the state</param>
        void Subscribe(INotificationSubscription subscription);
    }
}

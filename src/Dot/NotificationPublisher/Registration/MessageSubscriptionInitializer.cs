﻿using System.Collections.Generic;

namespace Cofoundry.Core.MessageAggregator.Internal
{
    /// <summary>
    /// Used to run all instances of IMessageSubscriptionRegistration registered
    /// with the DI container. Typically run when Cofoundry startsup
    /// </summary>
    public class MessageSubscriptionInitializer : IMessageSubscriptionInitializer
    {
        private readonly IMessageSubscriptionConfig _messageSubscriptionConfig;
        private readonly IEnumerable<IMessageSubscriptionRegistration> _registrations;

        public MessageSubscriptionInitializer(
            IMessageSubscriptionConfig messageSubscriptionConfig,
            IEnumerable<IMessageSubscriptionRegistration> registrations
            )
        {
            _messageSubscriptionConfig = messageSubscriptionConfig;
            _registrations = registrations;
        }

        /// <summary>
        /// Adds any bootstrappable message subscriptions to the registered IMessageAggregator
        /// instance.
        /// </summary>
        public void Initialize()
        {
            foreach (var registration in _registrations)
            {
                registration.Register(_messageSubscriptionConfig);
            }
        }
    }
}

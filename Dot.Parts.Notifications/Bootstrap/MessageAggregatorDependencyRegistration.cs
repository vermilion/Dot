using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.MessageAggregator.Internal;
using Cofoundry.Web;
using Dot.Configuration.Extensions;

namespace Cofoundry.Core.MessageAggregator.Registration
{
    public class MessageAggregatorDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.Startup(x =>
            {
                x.RegisterConfigurationTask<MessageAggregatorStartupConfigurationTask>();
            });

            container
                .RegisterSingleton<INotificationPublisherState>(new MessageAggregatorState())
                .Register<IMessageSubscriptionInitializer, MessageSubscriptionInitializer>()
                .Register<INotificationPublisher, Internal.MessageAggregator>()
                .Register<IMessageSubscriptionConfig, MessageSubscriptionConfig>();
                //.RegisterAll<IMessageSubscriptionRegistration>()
                //.RegisterAllGenericImplementations(typeof(INotificationHandler<>));
        }
    }
}

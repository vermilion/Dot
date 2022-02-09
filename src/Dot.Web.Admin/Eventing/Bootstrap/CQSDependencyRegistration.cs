using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain.CQS.Internal;

namespace Cofoundry.Domain.CQS.Registration
{
    public class CQSDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .Register<IMediator, Mediator>()
                .RegisterAllGenericImplementations(typeof(IRequestHandler<,>))
                .Register<IRequestHandlerFactory, RequestHandlerFactory>()
                .Register<ICommandLogService, DebugCommandLogService>()
                .Register<IExecutionContextFactory, ExecutionContextFactory>()
                ; 
        }
    }
}

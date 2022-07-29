using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain.CQS.Internal;

namespace Cofoundry.Domain.CQS.Registration
{
    public class CQSDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            // TODO: register all IRequestHandler<>

            container
                .Register<IMediator, Mediator>()
                .Register<IRequestHandlerFactory, RequestHandlerFactory>()
                .Register<ICommandLogService, DebugCommandLogService>()
                .Register<IExecutionContextFactory, ExecutionContextFactory>()
                ; 
        }
    }
}

using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing.Helpers
{
    internal abstract class RequestHandlerWrapper<TResponse> : RequestHandlerBase
    {
        public abstract Task<TResponse> Handle(IRequest<TResponse> request, ServiceFactory serviceFactory, CancellationToken cancellationToken);
    }

    internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override Task<object?> Handle(object request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
        {
            return Handle((IRequest<TResponse>)request, serviceFactory, cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        ExceptionDispatchInfo.Capture(t.Exception.InnerException).Throw();
                    }
                    return (object?)t.Result;
                }, cancellationToken);
        }

        public override Task<TResponse> Handle(IRequest<TResponse> request, ServiceFactory serviceFactory, CancellationToken cancellationToken)
        {
            var handler = GetHandler<IRequestHandler<TRequest, TResponse>>(serviceFactory);
            return handler.Handle((TRequest)request, cancellationToken);
        }
    }

    internal abstract class RequestHandlerBase
    {
        public abstract Task<object?> Handle(object request, ServiceFactory serviceFactory, CancellationToken cancellationToken);

        protected static THandler GetHandler<THandler>(ServiceFactory factory)
        {
            THandler handler;

            try
            {
                handler = factory.GetInstance<THandler>();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container. See the samples in GitHub for examples.", e);
            }

            if (handler == null)
            {
                throw new InvalidOperationException($"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container. See the samples in GitHub for examples.");
            }

            return handler;
        }
    }
}

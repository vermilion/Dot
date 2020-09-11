using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing.Helpers
{
    internal abstract class RequestHandlerWrapper<TResponse> : RequestHandlerBase
    {
        public abstract Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken,
            ServiceFactory serviceFactory);
    }

    internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override Task<object?> Handle(object request, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            return Handle((IRequest<TResponse>)request, cancellationToken, serviceFactory)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        ExceptionDispatchInfo.Capture(t.Exception.InnerException).Throw();
                    }
                    return (object?)t.Result;
                }, cancellationToken);
        }

        public override Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            var handler = GetHandler<IRequestHandler<TRequest, TResponse>>(serviceFactory);
            return handler.Handle((TRequest)request, cancellationToken);
        }
    }

    internal abstract class RequestHandlerBase
    {
        public abstract Task<object?> Handle(object request, CancellationToken cancellationToken, ServiceFactory serviceFactory);

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

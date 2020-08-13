using MediatR;
using System.Collections.Generic;

namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base select entity request type
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntitySelectRequest<TReadModel> : IRequest<IEnumerable<TReadModel>>
        where TReadModel : class
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Domain.CQS.Internal
{
    /// <summary>
    /// Factory to create IQueryHandler instances. This factory allows you to override
    /// or wrap the existing IQueryHandler implementation
    /// </summary>
    public interface IRequestHandlerFactory
    {
        /// <summary>
        /// Creates a new IAsyncQueryHandler instance with the specified type signature.
        /// </summary>
        IRequestHandler<TQuery, TResult> CreateAsyncHandler<TQuery, TResult>() where TQuery : IRequest<TResult>;
    }
}

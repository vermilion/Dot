using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Domain
{
    /// <summary>
    /// A useful collection of data access constructs from the Cofoundry
    /// CQS system in a single API to make it easier to work with and
    /// coordinate together. This also forms the basis of the IContentRepository.
    /// </summary>
    public interface IDomainRepository
    {
        /// <summary>
        /// Directly executes an IRequest instance and returns
        /// the results.
        /// </summary>
        /// <param name="query">Request to execute.</param>
        Task<TResult> ExecuteRequestAsync<TResult>(IRequest<TResult> query);
    }
}

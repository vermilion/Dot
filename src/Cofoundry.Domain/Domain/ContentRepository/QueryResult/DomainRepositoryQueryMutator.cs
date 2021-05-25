using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Domain
{
    public class DomainRepositoryQueryMutator<TQueryResult, TResult>
        : IDomainRepositoryQueryMutator<TQueryResult, TResult>
    {
        private readonly Func<Task<TResult>> _modifier;

        public DomainRepositoryQueryMutator(
            IRequest<TQueryResult> query,
            Func<Task<TResult>> modifier 
            )
        {
            Query = query;
            _modifier = modifier;
        }

        public IRequest<TQueryResult> Query { get; }

        public Task<TResult> ExecuteAsync()
        {
            return _modifier.Invoke();
        }
    }
}

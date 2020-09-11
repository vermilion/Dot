using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing.Decorators.DatabaseRetry
{
    [Mapping(typeof(DatabaseRetryAttribute))]
    public class DatabaseRetryRequestDecorator<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        private readonly IRequestHandler<TRequest, TResult> _handler;
        private readonly DatabaseRetryAttribute _databaseRetryOptions;

        public DatabaseRetryRequestDecorator(IRequestHandler<TRequest, TResult> handler, DatabaseRetryAttribute options)
        {
            _databaseRetryOptions = options;
            _handler = handler;
        }

        public async Task<TResult> Handle(TRequest query, CancellationToken cancellationToken)
        {
            int executedTimes = 0;
            TResult result;

            while (true)
            {
                try
                {
                    executedTimes++;
                    result = await _handler.Handle(query, cancellationToken).ConfigureAwait(false);
                    break;
                }
                catch (SqlException)
                {
                    if (executedTimes >= _databaseRetryOptions.RetryTimes)
                    {
                        throw;
                    }
                }
            }

            return result;
        }
    }
}
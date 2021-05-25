namespace Cofoundry.Domain.CQS
{
    /// <summary>
    /// Marker interface of a request object
    /// </summary>
    public interface IRequest
    {
    }

    /// <summary>
    /// Represents query parameters that when executed will yield an instance of TResult.
    /// </summary>
    /// <typeparam name="TResult">The type of result to returned from the query</typeparam>
    public interface IRequest<TResult> : IRequest
    {
    }
}

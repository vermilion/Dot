using Cofoundry.Core.Configuration;
using Cofoundry.Domain.CQS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.Core.Validation
{
    /// <summary>
    /// Service for validating models using DataAnnotation validation.
    /// </summary>
    public interface IModelValidationService
    {
        /// <summary>
        /// Validates the specific model and throws an exception if it is null or 
        /// contains any invalid properties.
        /// </summary>
        /// <typeparam name="TRequest">Type of the model to validate.</typeparam>
        /// <param name="model">The command to validate.</param>
        Task Validate<TRequest, TResponse>(TRequest model, IRequestHandler<TRequest, TResponse> handler, IExecutionContext executionContext)
             where TRequest : IRequest<TResponse>;

        //IReadOnlyCollection<ValidationError> GetErrors<TModel>(TModel settings) 
        //    where TModel : class;
    }
}

namespace Cofoundry.Core.Validation.Internal
{
    /// <summary>
    /// Service for validating models using DataAnnotation validation.
    /// </summary>
    public class ModelValidationService : IModelValidationService
    {
        /// <summary>
        /// Validates the specific model and throws an exception if it is null or 
        /// contains any invalid properties.
        /// </summary>
        /// <typeparam name="TRequest">Type of the model to validate.</typeparam>
        /// <param name="model">The command to validate.</param>
        /*public virtual async Task Validate<TRequest, TResponse>(TRequest model, IRequestHandler<TRequest, TResponse> handler, IExecutionContext executionContext)
             where TRequest : IRequest<TResponse>
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var cx = new ValidationContext(model);
            Validator.ValidateObject(model, cx, true);
        }*/
    }
}

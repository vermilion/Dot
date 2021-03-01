using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Validation
{
    public static class ValidationHelper
    {
        public static Task ValidateAndThrowAsync<T>(T model, Action<InlineValidator<T>> validate, CancellationToken cancellationToken = default)
        {
            var validator = new InlineValidator<T>();
            validate(validator);

            return validator.ValidateAndThrowAsync(model, cancellationToken: cancellationToken);
        }
    }

}
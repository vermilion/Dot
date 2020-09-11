using FluentValidation;
using System;
using System.Runtime.CompilerServices;

namespace PlatformFramework.Eventing.Decorators.Validation
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ValidationAttribute : DecoratorAttributeBase
    {
        public ValidationAttribute(Type validatorType, [CallerLineNumber] int order = 0)
            : base(order)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new TypeAccessException($"Invalid type passed. Expected type of {nameof(IValidator)}");
            }

            ValidatorType = validatorType;
        }

        public Type ValidatorType { get; }
    }
}

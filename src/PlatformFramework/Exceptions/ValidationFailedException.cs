using System;
using System.Collections.Generic;
using System.Linq;
using PlatformFramework.Validation;

namespace PlatformFramework.Exceptions
{
    /// <summary>
    /// Framework validation exception
    /// </summary>
    [Serializable]
    public class ValidationFailedException : FrameworkException
    {
        public IReadOnlyList<ValidationError> Failures { get; }

        public ValidationFailedException(string message)
            : this(message, Enumerable.Empty<ValidationError>())
        {
        }

        public ValidationFailedException(string message, IEnumerable<ValidationError> failures)
            : base(message)
        {
            Failures = failures.ToList();
        }

        public override Dictionary<string, object> Properties()
        {
            return new Dictionary<string, object>
            {
                { "errors", Failures }
            };
        }
    }
}
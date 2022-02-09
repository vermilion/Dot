using System;
using System.Collections.Generic;
using System.Linq;

namespace Cofoundry.Core.Validation
{
    /// <summary>
    /// Framework validation exception
    /// </summary>
    public class ValidationFailedException : Exception
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
    }
}
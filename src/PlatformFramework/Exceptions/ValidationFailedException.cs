using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatformFramework.Shared.Exceptions
{
    [Serializable]
    public class ValidationFailedException : FrameworkException
    {
        public IReadOnlyList<ValidationFailure> Failures { get; }

        public ValidationFailedException(string message)
            : this(message, Enumerable.Empty<ValidationFailure>())
        {
        }

        public ValidationFailedException(string message, IEnumerable<ValidationFailure> failures)
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
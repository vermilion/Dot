using System;
using System.Collections.Generic;

namespace PlatformFramework.Shared.Exceptions
{
    public class FrameworkException : Exception
    {
        public FrameworkException()
        {
        }

        public FrameworkException(string message)
            : base(message)
        {
        }

        public FrameworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public virtual Dictionary<string, object> Properties()
        {
            return new Dictionary<string, object>();
        }
    }
}
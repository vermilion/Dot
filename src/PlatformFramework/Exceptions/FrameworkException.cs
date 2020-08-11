using System;
using System.Collections.Generic;

namespace PlatformFramework.Exceptions
{
    /// <summary>
    /// Default Excrption that may be raised from Framework
    /// </summary>
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

        /// <summary>
        /// Get value properties of an Exception
        /// </summary>
        /// <returns>List of properties</returns>
        public virtual Dictionary<string, object> Properties()
        {
            return new Dictionary<string, object>();
        }
    }
}
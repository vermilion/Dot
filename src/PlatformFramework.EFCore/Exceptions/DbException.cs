using System;

namespace PlatformFramework.Shared.Exceptions
{
    [Serializable]
    public class DbException : FrameworkException
    {
        public DbException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
using System;
using PlatformFramework.Exceptions;

namespace PlatformFramework.EFCore.Exceptions
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
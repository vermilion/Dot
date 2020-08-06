using System;

namespace PlatformFramework.EFCore.Exceptions
{
    [Serializable]
    public class DbConcurrencyException : DbException
    {
        public DbConcurrencyException()
            : this("The record has been modified since it was loaded. The operation was canceled!", null!)
        {
        }

        public DbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
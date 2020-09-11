using System;
using System.Runtime.CompilerServices;

namespace PlatformFramework.Eventing.Decorators.DatabaseRetry
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DatabaseRetryAttribute : DecoratorAttributeBase
    {
        public int RetryTimes { get; }

        public DatabaseRetryAttribute(int retryTimes = 3, [CallerLineNumber] int order = 0)
            : base(order)
        {
            RetryTimes = retryTimes;
        }
    }
}

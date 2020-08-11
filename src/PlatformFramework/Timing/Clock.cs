using System;
using PlatformFramework.Abstractions;

namespace PlatformFramework.Timing
{
    /// <summary>
    /// Default <see cref="IClockProvider"/> implementation
    /// </summary>
    internal sealed class ClockProvider : IClockProvider
    {
        public DateTime Now => DateTime.UtcNow;

        public DateTime Normalize(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }
    }
}
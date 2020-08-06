using System;
using PlatformFramework.Interfaces.Timing;

namespace PlatformFramework.Timing
{
    /// <summary>
    /// Default <see cref="IClockProvider"/> implementation
    /// </summary>
    internal sealed class ClockProvider : IClockProvider
    {
        public DateTime Now => SystemTime.Now();

        public DateTime Normalize(DateTime dateTime)
        {
            return SystemTime.Normalize(dateTime);
        }
    }
}
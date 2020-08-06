using System;

namespace PlatformFramework.Interfaces.Timing
{
    /// <summary>
    /// Defines the required contract for implementing a clock.
    /// </summary>
    public interface IClockProvider
    {
        DateTime Now { get; }
        DateTime Normalize(DateTime dateTime);
    }
}
using System;

namespace PlatformFramework.Abstractions
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
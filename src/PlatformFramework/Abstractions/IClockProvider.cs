using System;

namespace PlatformFramework.Abstractions
{
    /// <summary>
    /// Defines the required contract for implementing a clock
    /// </summary>
    public interface IClockProvider
    {
        /// <summary>
        /// Current
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Normalizes passed datetime
        /// </summary>
        /// <param name="dateTime">DateTime value</param>
        /// <returns>Normalized value</returns>
        DateTime Normalize(DateTime dateTime);
    }
}
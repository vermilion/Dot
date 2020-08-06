using System;

namespace PlatformFramework.Timing
{
    public static class SystemTime
    {
        public static readonly Func<DateTime> Now = () => DateTime.UtcNow;

        public static readonly Func<DateTime, DateTime> Normalize = dateTime => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}
using System;

namespace PlatformFramework.Shared.Exceptions
{
    /// <summary>
    /// User-readable exception type
    /// </summary>
    [Serializable]
    public class UserFriendlyException : FrameworkException
    {
        public UserFriendlyException(string message) 
            : base(message)
        {
        }
    }
}
using System;

namespace PlatformFramework.Shared.Exceptions
{
    [Serializable]
    public class UserFriendlyException : FrameworkException
    {
        public UserFriendlyException(string message) 
            : base(message)
        {
        }
    }
}
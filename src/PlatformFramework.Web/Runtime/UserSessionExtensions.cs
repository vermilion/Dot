using System;
using PlatformFramework.Interfaces.Runtime;
using PlatformFramework.Shared.Extensions;

namespace PlatformFramework.Web.Runtime
{
    public static class UserSessionExtensions
    {
        public static T UserId<T>(this IUserSession session)
        {
            if (string.IsNullOrEmpty(session.UserId))
                throw new InvalidOperationException("This IUserSession.UserId is Null or Empty");

            return session.UserId.To<T>();
        }

        public static T ImpersonatorUserId<T>(this IUserSession session)
        {
            if (string.IsNullOrEmpty(session.ImpersonatorUserId))
                throw new InvalidOperationException("This IUserSession.ImpersonatorUserId is Null or Empty");

            return session.ImpersonatorUserId.To<T>();
        }
    }
}
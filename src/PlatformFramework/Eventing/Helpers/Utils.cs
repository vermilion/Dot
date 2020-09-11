using System;

namespace PlatformFramework.Eventing.Helpers
{
    internal static class Utils
    {
        public static bool IsHandlerInterface(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            var typeDefinition = type.GetGenericTypeDefinition();

            return typeDefinition == typeof(IRequestHandler<,>);
        }
    }
}

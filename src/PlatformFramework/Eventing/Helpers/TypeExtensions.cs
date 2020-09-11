﻿using System;

namespace PlatformFramework.Eventing.Helpers
{
    internal static class TypeExtensions
    {
        public static bool HasInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfacesOf(interfaceType).Length > 0;
        }

        public static Type[] GetInterfacesOf(this Type type, Type interfaceType)
        {
            return type.FindInterfaces((i, _) => i.GetGenericTypeDefinitionSafe() == interfaceType, null);
        }

        public static Type GetGenericTypeDefinitionSafe(this Type type)
        {
            return type.IsGenericType
                ? type.GetGenericTypeDefinition()
                : type;
        }

        public static Type MakeGenericTypeSafe(this Type type, params Type[] typeArguments)
        {
            return type.IsGenericType && type.GenericTypeArguments.Length == 0
                ? type.MakeGenericType(typeArguments)
                : type;
        }
    }
}

using PlatformFramework.Eventing.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PlatformFramework.Eventing.Decorators
{
    public static class Mappings
    {
        static Mappings()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.HasInterface(typeof(IRequestHandler<,>)))
                {
                    var decoratorAttribute = (MappingAttribute?)Array.Find(type.GetCustomAttributes(false), x => x.GetType() == typeof(MappingAttribute));

                    if (decoratorAttribute != null)
                    {
                        AttributeToRequestHandler[decoratorAttribute.Type] = type;
                    }
                }
            }
        }

        public static readonly Dictionary<Type, Type> AttributeToRequestHandler = new Dictionary<Type, Type>();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MappingAttribute : Attribute
    {
        public MappingAttribute(Type type)
        {
            Type = type;
        }

        internal Type Type { get; set; }
    }
}
using System;
using System.Runtime.CompilerServices;

namespace PlatformFramework.Eventing.Decorators
{
    public abstract class DecoratorAttributeBase : Attribute
    {
        protected DecoratorAttributeBase([CallerLineNumber] int order = 0)
        {
            Order = order;
        }

        public int Order { get; set; }
    }
}
using System;
using System.Runtime.CompilerServices;

namespace PlatformFramework.Eventing.Decorators.AuditLog
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AuditLogAttribute : DecoratorAttributeBase
    {
        public AuditLogAttribute([CallerLineNumber] int order = 0)
            : base(order)
        {

        }
    }
}

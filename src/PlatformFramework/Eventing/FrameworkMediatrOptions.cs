using PlatformFramework.Eventing.Behaviors;
using System;
using System.Collections.Generic;

namespace PlatformFramework.Eventing
{
    public class FrameworkMediatrOptions
    {
        public ICollection<Type> Behaviors = new List<Type>(new[]
        {
            typeof(LoggingPipelineBehavior<,>),
            typeof(ValidationPipelineBehavior<,>)
        });
    }
}
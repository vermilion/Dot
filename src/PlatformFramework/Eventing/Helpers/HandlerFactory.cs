using PlatformFramework.Eventing.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlatformFramework.Eventing.Helpers
{
    internal class HandlerFactory
    {
        private readonly List<Func<object, Type, IServiceProvider, object>> _handlerFactoriesPipeline = new List<Func<object, Type, IServiceProvider, object>>();

        public HandlerFactory(Type type)
        {
            AddHandlerFactory(type);
            AddDecoratedFactories(type);
        }

        public object Create(IServiceProvider provider, Type handlerInterfaceType)
        {
            object currentHandler = null!;
            foreach (var handlerFactory in _handlerFactoriesPipeline)
            {
                currentHandler = handlerFactory(currentHandler, handlerInterfaceType, provider);
            }

            return currentHandler!;
        }

        private void AddDecoratedFactories(Type type)
        {
            var attributes = type
                .GetCustomAttributes<DecoratorAttributeBase>(inherit: false)
                .OrderBy(x => x.Order)
                .ToArray();

            for (var i = attributes.Length - 1; i >= 0; i--)
            {
                var attribute = attributes[i];
                var attributeType = attribute.GetType();
                Type? decoratorHandlerType = null;
                var hasDecoratorHandler = type.HasInterface(typeof(IRequestHandler<,>)) && Mappings.AttributeToRequestHandler.TryGetValue(attributeType, out decoratorHandlerType);

                if (!hasDecoratorHandler || decoratorHandlerType == null)
                {
                    continue;
                }

                AddHandlerFactory(decoratorHandlerType, attribute);
            }
        }

        private void AddHandlerFactory(Type handlerType, object? attribute = null)
        {
            _handlerFactoriesPipeline.Add(CreateHandler);

            object CreateHandler(object decoratingHandler, Type interfaceType, IServiceProvider provider)
            {
                var ctor = handlerType
                   .MakeGenericTypeSafe(interfaceType.GenericTypeArguments)
                   .GetConstructors()
                   .Single();

                var parameterInfos = ctor.GetParameters();
                var parameters = GetParameters(parameterInfos, decoratingHandler, attribute, provider);

                return ctor.Invoke(parameters);
            }
        }

        private static object[] GetParameters(IEnumerable<ParameterInfo> parameterInfos, object current, object? attribute, IServiceProvider provider)
        {
            return parameterInfos.Select(GetParameter).ToArray();

            object GetParameter(ParameterInfo parameterInfo)
            {
                var parameterType = parameterInfo.ParameterType;

                if (Utils.IsHandlerInterface(parameterType))
                {
                    return current;
                }

                if (parameterType == attribute?.GetType())
                {
                    return attribute;
                }

                var service = provider.GetService(parameterType);
                if (service != null)
                {
                    return service;
                }

                throw new ArgumentException($"Type {parameterType} not found");
            }
        }
    }
}

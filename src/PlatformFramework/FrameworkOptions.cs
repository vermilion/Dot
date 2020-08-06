using System;
using System.Collections.Generic;
using System.Reflection;

namespace PlatformFramework
{
    /// <summary>
    /// Настройки Framework
    /// </summary>
    public class FrameworkOptions
    {
        /// <summary>
        /// Список сборок приложения
        /// </summary>
        public List<Assembly> Assemblies { get; } = new List<Assembly>(new[] { Assembly.GetEntryAssembly() });

        internal Dictionary<Type, Delegate> ConfigureActions { get; } = new Dictionary<Type, Delegate>();

        /// <summary>
        /// Конфигурирование части Framework
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="action">Метод конфигурирования</param>
        public void Configure<T>(Action<T> action)
        {
            ConfigureActions[typeof(T)] = action;
        }
    }
}
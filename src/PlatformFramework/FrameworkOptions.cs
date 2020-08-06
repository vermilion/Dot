using System;
using System.Collections.Generic;
using System.Reflection;

namespace PlatformFramework
{
    /// <summary>
    /// Framework configuration options
    /// </summary>
    public class FrameworkOptions
    {
        /// <summary>
        /// Assemblies used for framework parts
        /// </summary>
        public List<Assembly> Assemblies { get; } = new List<Assembly>(new[] { Assembly.GetEntryAssembly() });

        internal Dictionary<Type, Delegate> ConfigureActions { get; } = new Dictionary<Type, Delegate>();

        /// <summary>
        /// Framework part configuration
        /// </summary>
        /// <typeparam name="T">Part Type</typeparam>
        /// <param name="action">Configuration action</param>
        public void Configure<T>(Action<T> action)
        {
            ConfigureActions[typeof(T)] = action;
        }
    }
}
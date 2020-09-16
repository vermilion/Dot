using PlatformFramework.Abstractions;
using System.Collections.Generic;

namespace PlatformFramework
{
    /// <summary>
    /// Framework configuration options
    /// </summary>
    public class FrameworkOptions
    {
        internal List<IFrameworkModule> Modules { get; } = new List<IFrameworkModule>();

        /// <summary>
        /// Adds Framework module configuration
        /// </summary>
        /// <typeparam name="T">Module Type</typeparam>
        public void AddModule<T>()
            where T: IFrameworkModule, new()
        {
            var module = new T();
            Modules.Add(module);
        }
    }
}
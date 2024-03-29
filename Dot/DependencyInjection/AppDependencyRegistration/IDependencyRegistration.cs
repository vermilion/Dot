﻿namespace Cofoundry.Core.DependencyInjection
{
    /// <summary>
    /// Interface to mark classes that bootstrap DI container registration. 
    /// Implementations will be automatically scanned for and registered at 
    /// startup when using the default Cofoundry application startup process
    /// </summary>
    public interface IDependencyRegistration
    {
        void Register(IContainerRegister container);
    }
}

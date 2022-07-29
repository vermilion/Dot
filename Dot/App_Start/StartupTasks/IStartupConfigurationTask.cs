﻿using Microsoft.AspNetCore.Builder;

namespace Cofoundry.Web
{
    /// <summary>
    /// Represents a task that runs in the Cofoundry startup and 
    /// initialization pipeline.
    /// </summary>
    public interface IStartupConfigurationTask
    {
        /// <summary>
        /// Executes the configuration task
        /// </summary>
        void Configure(IApplicationBuilder app);
    }
}
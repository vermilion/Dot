﻿using Cofoundry.Core.Configuration;

namespace Cofoundry.Core.AutoUpdate
{
    /// <summary>
    /// Settings for configuring the auto-update process. Used specifically in
    /// AutoUpdateService.
    /// </summary>
    public class AutoUpdateSettings : CofoundryConfigurationSettingsBase
    {
        public AutoUpdateSettings()
        {
            ProcessLockTimeoutInSeconds = 600;
            RequestWaitForCompletionTimeInSeconds = 15;
        }

        /// <summary>
        /// Disables the auto-update process entirely.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// This is the amount of time before the process lock expires and
        /// allows another auto-update process to start. This is designed to
        /// prevent multiple auto-update processes running concurrently in multi-instance
        /// deployment scenarios. By default this is set to 10 minutes which should be
        /// more than enough time for the process to run, but you may wish to shorten/lengthen 
        /// this depending on your needs.
        /// </summary>
        public int ProcessLockTimeoutInSeconds { get; set; }

        /// <summary>
        /// The amount of time (in seconds) that a request should pause
        /// and wait for the auto-update process to complete before 
        /// returning a 503 "temporarily unavailable" response to the
        /// client. This defaults to 15 seconds. Setting this to 0 will
        /// cause the process not to wait.
        /// </summary>
        public int RequestWaitForCompletionTimeInSeconds { get; set; }
    }
}

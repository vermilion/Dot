﻿using System;

namespace Cofoundry.Domain.CQS.Internal
{
    /// <summary>
    /// A snapshot of the context in which a Command or Query should be executed.
    /// </summary>
    public class ExecutionContext : IExecutionContext
    {
        /// <summary>
        /// The user that the Command/Query should be executed as.
        /// </summary>
        public IUserContext UserContext { get; set; }

        /// <summary>
        /// The datetime that the Commnad/Query has been executed by the user. If the Command execution
        /// is deferred then this date may appear in the past and won't be equivalent to DateTime.UtcNow.
        /// </summary>
        public DateTime ExecutionDate { get; set; }
    }
}

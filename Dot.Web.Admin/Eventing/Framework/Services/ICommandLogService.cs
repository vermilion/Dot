﻿using System;
using System.Threading.Tasks;

namespace Cofoundry.Domain.CQS
{
    /// <summary>
    /// Service for logging audit data about executed commands.
    /// </summary>
    /// <remarks>
    /// Typically only commands which implement ILoggable should be logged. Commands
    /// that don't implement ILoggableCommand may bloat the log or contain sensitive data.
    /// </remarks>
    public interface ICommandLogService
    {
        Task LogAsync<TCommand>(TCommand command, IExecutionContext executionContext) where TCommand : IRequest;

        Task LogFailedAsync<TCommand>(TCommand command, IExecutionContext executionContext, Exception ex = null) where TCommand : IRequest;
    }
}

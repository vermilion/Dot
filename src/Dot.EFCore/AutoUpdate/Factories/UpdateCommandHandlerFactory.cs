using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cofoundry.Core.AutoUpdate
{
    /// <summary>
    /// Factory that creates concrete implementations of IUpdateCommand handlers.
    /// </summary>
    public class UpdateCommandHandlerFactory : IUpdateCommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateCommandHandlerFactory(
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }

        public IVersionedUpdateCommandHandler<TCommand> CreateVersionedCommand<TCommand>() where TCommand : IVersionedUpdateCommand
        {
            var asyncCommand = _serviceProvider.GetService<IVersionedUpdateCommandHandler<TCommand>>();
            return asyncCommand;
        }

        public IAlwaysRunUpdateCommandHandler<TCommand> CreateAlwaysRunCommand<TCommand>() where TCommand : IAlwaysRunUpdateCommand
        {
            var asyncCommand = _serviceProvider.GetService<IAlwaysRunUpdateCommandHandler<TCommand>>();
            return asyncCommand;
        }
    }
}

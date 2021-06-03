using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Core.AutoUpdate;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain.Installation
{
    /// <summary>
    /// Runs the RegisterEntityDefinitionsCommand at startup, adding new entity
    /// definitions defined in code to the system and initializing permissions.
    /// </summary>
    public class RegisterPermissionsAndRolesUpdateCommandHandler : IAlwaysRunUpdateCommandHandler<RegisterPermissionsAndRolesUpdateCommand>
    {
        private readonly IMediator _mediator;
        private readonly IExecutionContextFactory _executionContextFactory;

        public RegisterPermissionsAndRolesUpdateCommandHandler(
            IMediator mediator,
            IExecutionContextFactory executionContextFactory
            )
        {
            _mediator = mediator;
            _executionContextFactory = executionContextFactory;
        }

        public async Task ExecuteAsync(RegisterPermissionsAndRolesUpdateCommand command)
        {
            var cx = await _executionContextFactory.CreateSystemUserExecutionContextAsync();
            await _mediator.ExecuteAsync(new RegisterPermissionsAndRolesCommand(), cx);
        }
    }
}

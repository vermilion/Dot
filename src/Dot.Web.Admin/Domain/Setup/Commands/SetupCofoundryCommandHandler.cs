using Cofoundry.Core;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.Caching.Services;
using Dot.EFCore.UnitOfWork;
using Dot.Web.Admin.Domain.Setup.Models;
using Microsoft.EntityFrameworkCore;
using ExecutionContext = Cofoundry.Domain.CQS.Internal.ExecutionContext;

namespace Cofoundry.Domain.Internal
{
    public class SetupCofoundryCommandHandler
        : IRequestHandler<SetupCofoundryCommand, Unit>
    {
        #region constructor

        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserContextMapper _userContextMapper;
        private readonly IObjectCacheFactory _objectCacheFactory;

        public SetupCofoundryCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork,
            UserContextMapper userContextMapper,
            IObjectCacheFactory objectCacheFactory
            )
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _userContextMapper = userContextMapper;
            _objectCacheFactory = objectCacheFactory;
        }

        #endregion

        #region Execute

        public async Task<Unit> ExecuteAsync(SetupCofoundryCommand command, IExecutionContext executionContext)
        {
            var settings = await _mediator.ExecuteAsync(new GetSettingsQuery<InternalSettings>());

            if (settings.IsSetup)
            {
                throw new InvalidOperationException("Site is already set up.");
            }

            using (var scope = _unitOfWork.BeginTransaction())
            {
                var userId = await CreateAdminUser(command);
                var impersonatedUserContext = await GetImpersonatedUserContext(executionContext, userId);

                var settingsCommand = await _mediator.ExecuteAsync(new GetUpdateCommandQuery<UpdateGeneralSiteSettingsCommand>());
                settingsCommand.ApplicationName = command.ApplicationName;
                await _mediator.ExecuteAsync(settingsCommand, impersonatedUserContext);

                // Take the opportunity to break the cache in case any additional install scripts have been run since initialization
                _objectCacheFactory.Clear();

                // Setup Complete
                await _mediator.ExecuteAsync(new MarkAsSetUpCommand(), impersonatedUserContext);

                await scope.CompleteAsync();
            }

            return Unit.Value;
        }

        private async Task<ExecutionContext>  GetImpersonatedUserContext(IExecutionContext executionContext, int userId)
        {
            var dbUser = await _unitOfWork
                .Users()
                .Include(u => u.Role)
                .FilterById(userId)
                .SingleOrDefaultAsync();

            EntityNotFoundException.ThrowIfNull(dbUser, userId);
            var impersonatedUserContext = _userContextMapper.Map(dbUser);

            var impersonatedExecutionContext = new ExecutionContext()
            {
                ExecutionDate = executionContext.ExecutionDate,
                UserContext = impersonatedUserContext
            };

            return impersonatedExecutionContext;
        }

        #endregion

        #region private helpers

        private async Task<int> CreateAdminUser(SetupCofoundryCommand command)
        {
            /*var newUserCommand = new AddMasterCofoundryUserCommand
            {
                Email = command.UserEmail,
                FirstName = command.UserFirstName,
                LastName = command.UserLastName,
                Password = command.UserPassword,
                RequirePasswordChange = command.RequirePasswordChange
            };
            await _mediator.ExecuteAsync(newUserCommand);

            return newUserCommand.OutputUserId;*/
            throw new NotImplementedException(); // TODO
        }

        #endregion
    }
}

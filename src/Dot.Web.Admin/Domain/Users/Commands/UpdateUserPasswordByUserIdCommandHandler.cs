using System.Threading.Tasks;
using Cofoundry.Core;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    public class UpdateUserPasswordByUserIdCommandHandler
        : IRequestHandler<UpdateUserPasswordByUserIdCommand, Unit>
    {
        private readonly IPasswordUpdateCommandHelper _passwordUpdateCommandHelper;
        private readonly IUnitOfWork _unitOfWork;
        
        #region construstor

        public UpdateUserPasswordByUserIdCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordUpdateCommandHelper passwordUpdateCommandHelper
            )
        {
            _unitOfWork = unitOfWork;
            _passwordUpdateCommandHelper = passwordUpdateCommandHelper;
        }

        #endregion

        #region execution

        public async Task<Unit> ExecuteAsync(UpdateUserPasswordByUserIdCommand command, IExecutionContext executionContext)
        {
            var user = await GetUser(command.UserId);
            EntityNotFoundException.ThrowIfNull(user, command.UserId);

            _passwordUpdateCommandHelper.ValidatePermissions(executionContext);

            _passwordUpdateCommandHelper.UpdatePassword(command.NewPassword, user, executionContext);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }

        #endregion
        
        private Task<User> GetUser(int userId)
        {
            return _unitOfWork
                .Users()
                .FilterById(userId)
                .FilterCanLogIn()
                .SingleOrDefaultAsync();
        }
    }
}

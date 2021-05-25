using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    public class ResetUserPasswordByUsernameCommandHandler 
        : IRequestHandler<ResetUserPasswordByUsernameCommand, Unit>
    {
        #region construstor
        
        private readonly CofoundryDbContext _dbContext;
        private readonly IResetUserPasswordCommandHelper _resetUserPasswordCommandHelper;

        public ResetUserPasswordByUsernameCommandHandler(
            CofoundryDbContext dbContext,
            IResetUserPasswordCommandHelper resetUserPasswordCommandHelper
            )
        {
            _dbContext = dbContext;
            _resetUserPasswordCommandHelper = resetUserPasswordCommandHelper; 
        }

        #endregion

        #region execution

        public async Task<Unit> ExecuteAsync(ResetUserPasswordByUsernameCommand command, IExecutionContext executionContext)
        {
            await _resetUserPasswordCommandHelper.ValidateCommandAsync(command, executionContext);
            var user = await QueryUser(command, executionContext).SingleOrDefaultAsync();
            if (user == null) return Unit.Value;
            await _resetUserPasswordCommandHelper.ResetPasswordAsync(user, command, executionContext);

            return Unit.Value;
        }

        #endregion

        #region private helpers

        private IQueryable<User> QueryUser(ResetUserPasswordByUsernameCommand command, IExecutionContext executionContext)
        {
            var user = _dbContext
                .Users
                .FilterCanLogIn()
                .Where(u => u.Username == command.Username.Trim());

            return user;
        }

        #endregion
    }
}

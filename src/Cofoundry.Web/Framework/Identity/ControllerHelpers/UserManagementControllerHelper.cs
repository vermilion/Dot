using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Web.Identity
{
    /// <summary>
    /// A helper class with shared functionality between controllers
    /// that manage the currently logged in users account.
    /// </summary>
    public class UserManagementControllerHelper
    {
        private readonly ICommandExecutor _commandExecutor;
        #region constructor

        public UserManagementControllerHelper(
            ICommandExecutor commandExecutor
            )
        {
            _commandExecutor = commandExecutor;
        }

        #endregion

        #region delete user

        public Task DeleteUserAsync(Controller controller, int userId)
        {
            var command = new DeleteUserCommand();
            command.UserId = userId;

            if (controller.ModelState.IsValid)
            {
                return _commandExecutor.ExecuteAsync(command);
            }

            throw new Exception();
        }

        #endregion
    }
}
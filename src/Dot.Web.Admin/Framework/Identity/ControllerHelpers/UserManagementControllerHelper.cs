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
        private readonly IMediator _mediator;
        #region constructor

        public UserManagementControllerHelper(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        #endregion

        #region delete user

        public Task DeleteUserAsync(Controller controller, int userId)
        {
            var command = new DeleteUserCommand();
            command.UserId = userId;

            if (controller.ModelState.IsValid)
            {
                return _mediator.ExecuteAsync(command);
            }

            throw new Exception();
        }

        #endregion
    }
}
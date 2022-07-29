using System.ComponentModel.DataAnnotations;
using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Deletes a role with the specified database id. Roles cannot be
    /// deleted if assigned to users.
    /// </summary>
    public class DeleteRoleCommand : IRequest<Unit>, ILoggableCommand
    {
        public DeleteRoleCommand() { }

        /// <summary>
        /// Initialized the command with the specified roleId
        /// </summary>
        /// <param name="roleId">Id of the role to delete.</param>
        public DeleteRoleCommand(int roleId)
        {
            RoleId = roleId;
        }

        /// <summary>
        /// Id of the role to delete.
        /// </summary>
        [Required]
        public int RoleId { get; set; }
    }
}

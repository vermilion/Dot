using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Adds a new role to a user area with a specific set of permissions.
    /// </summary>
    public class AddRoleCommand : IRequest<AddRoleCommandResult>, ILoggableCommand
    {
        /// <summary>
        /// A user friendly title for the role. Role titles must be unique 
        /// per user area and up to 50 characters.
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The permissions to add the role when it is created.
        /// </summary>
        public ICollection<PermissionCommandData> Permissions { get; set; }
    }

    public class AddRoleCommandResult
    {
        /// <summary>
        /// The database id of the newly created role. This is set after the command
        /// has been run.
        /// </summary>
        public int RoleId { get; set; }
    }
}

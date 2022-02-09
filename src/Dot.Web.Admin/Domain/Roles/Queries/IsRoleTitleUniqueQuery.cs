using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Determines if a role title is unique
    /// </summary>
    public class IsRoleTitleUniqueQuery : IRequest<bool>
    {
        /// <summary>
        /// Optional database id of an existing role to exclude from the uniqueness 
        /// check. Use this when checking the uniqueness of an existing Role.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// The role title to check for uniqueness (not case sensitive).
        /// </summary>
        public string Title { get; set; }
    }
}

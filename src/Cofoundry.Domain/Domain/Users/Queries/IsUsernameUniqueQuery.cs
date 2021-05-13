using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Determines if a username is unique within a specific UserArea.
    /// Usernames only have to be unique per UserArea.
    /// </summary>
    public class IsUsernameUniqueQuery : IQuery<bool>
    {
        /// <summary>
        /// Optional database id of an existing user to exclude from the uniqueness 
        /// check. Use this when checking the uniqueness of an existing user.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// The username to check for uniqueness (not case sensitive).
        /// </summary>
        public string Username { get; set; }
    }
}

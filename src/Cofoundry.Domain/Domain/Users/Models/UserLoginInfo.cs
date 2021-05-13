namespace Cofoundry.Domain
{
    /// <summary>
    /// User information relating to a login request
    /// </summary>
    public class UserLoginInfo
    {
        /// <summary>
        /// Database id of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// True if a password change is required, this is set to true when an account is
        /// first created.
        /// </summary>
        public bool RequirePasswordChange { get; set; }
    }
}

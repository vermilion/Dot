namespace Cofoundry.Domain
{
    /// <summary>
    /// Service for hashing and verifying user passwords.
    /// </summary>
    public interface IPasswordCryptographyService
    {
        /// <summary>
        /// Verifies that an unhashed password matches the specified hash.
        /// </summary>
        /// <param name="password">Plain text version of the password to check</param>
        /// <param name="hash">The hash to check the password against</param>
        bool Verify(string password, string hash);

        /// <summary>
        /// Creates a hash from the specified password string.
        /// </summary>
        /// <param name="password">Password to hash.</param>
        PasswordCryptographyResult CreateHash(string password);
    }
}

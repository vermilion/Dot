namespace Cofoundry.Domain
{
    /// <summary>
    /// The anonymous role is used for any unathenticated requests.
    /// Having an anonymous role allows you to configure permission
    /// for anyone not logged into the application.
    /// </summary>
    public class AnonymousRole : IRoleDefinition
    {
        /// <summary>
        /// Constant value for the anonymous role code
        /// </summary>
        public const string AnonymousRoleCode = "ANO";

        public string Title { get { return "Anonymous"; } }

        public string RoleCode { get { return AnonymousRoleCode; } }

    }
}

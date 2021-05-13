namespace Cofoundry.Web.Identity
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public bool RequiresPasswordChange { get; set; }
    }
}
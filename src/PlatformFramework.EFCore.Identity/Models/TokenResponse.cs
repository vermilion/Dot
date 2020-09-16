namespace PlatformFramework.EFCore.Identity.Models
{
    public class TokenResponse
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

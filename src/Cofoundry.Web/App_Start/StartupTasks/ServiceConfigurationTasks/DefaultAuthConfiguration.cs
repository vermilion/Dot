using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Cofoundry.Web
{
    /// <summary>
    /// The default auth configuration adds cookie auth for each of the user areas.
    /// </summary>
    public class DefaultAuthConfiguration : IAuthConfiguration
    {
        private readonly IAuthCookieNamespaceProvider _authCookieNamespaceProvider;

        public DefaultAuthConfiguration(
            IAuthCookieNamespaceProvider authCookieNamespaceProvider
            )
        {
            _authCookieNamespaceProvider = authCookieNamespaceProvider;
        }

        public void Configure(IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            var authBuilder = mvcBuilder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            var cookieNamespace = _authCookieNamespaceProvider.GetNamespace();

            authBuilder
                .AddCookie(cookieOptions =>
                {
                    cookieOptions.Cookie.Name = cookieNamespace;
                    cookieOptions.Cookie.HttpOnly = true;
                    cookieOptions.Cookie.IsEssential = true;
                    cookieOptions.Cookie.SameSite = SameSiteMode.Lax;
                });

            mvcBuilder.Services.AddAuthorization();
        }
    }
}

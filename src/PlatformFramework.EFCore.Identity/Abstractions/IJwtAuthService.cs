using PlatformFramework.EFCore.Identity.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Abstrations
{
    public interface IJwtAuthService
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        JwtAuthResult GenerateTokens(string username, IEnumerable<Claim> claims, DateTime now);
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);
        void RemoveExpiredRefreshTokens(DateTime now);
        Task RemoveRefreshTokenByUserName(string userName);
        (ClaimsPrincipal, JwtSecurityToken?) DecodeJwtToken(string token);
    }
}

using System.Text.Json.Serialization;

namespace PlatformFramework.EFCore.Identity.Models
{
    /// <summary>
    /// Default JWT configuration
    /// </summary>
    public class JwtTokenConfig
    {
        /// <summary>
        /// Gets or sets the secret that is to be used for signature validation
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="string"/> that represents a valid issuer that will be used to check against the token's issuer
        /// </summary>
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="string"/> that represents a valid audience that will be used to check against the token's audience
        /// </summary>
        [JsonPropertyName("audience")]
        public string Audience { get; set; }

        [JsonPropertyName("accessTokenExpiration")]
        public int AccessTokenExpiration { get; set; }

        [JsonPropertyName("refreshTokenExpiration")]
        public int RefreshTokenExpiration { get; set; }
    }
}

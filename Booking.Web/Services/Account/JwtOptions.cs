
namespace Booking.Web.Services.Account
{
    public sealed class JwtOptions
    {
		public string SigningKey { get; init; } 
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public float AccessTokenExpiryInMinutes { get; init; }

        public float RefreshTokenExpiryInMinutes { get; init; }

        public JwtOptions(string signingKey, string issuer, string audience, float accessTokenExpiryInMinutes, float refreshTokenExpiryInMinutes)
        {
            SigningKey = signingKey;
            Issuer = issuer;
            Audience = audience;
            AccessTokenExpiryInMinutes = accessTokenExpiryInMinutes;
            RefreshTokenExpiryInMinutes = refreshTokenExpiryInMinutes;
        }
    }
}

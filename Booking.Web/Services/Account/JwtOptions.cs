
namespace Booking.Web.Services.Account
{
    public sealed class JwtOptions
    {
		public string SigningKey { get; init; } 
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int TokenExpiryInMinutes { get; init; }

        public JwtOptions(string signingKey, string issuer, string audience, int tokenExpiryInMinutes)
        {
            SigningKey = signingKey;
            Issuer= issuer;
            Audience= audience;
            TokenExpiryInMinutes = tokenExpiryInMinutes;
        }
    }
}

using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.Web.Services.Account
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
		}

        public JwtTokenResult Generate(User user)
        {
            var expiration = TimeSpan.FromMinutes(_jwtOptions.TokenExpiryInMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(), ClaimValueTypes.UInteger64)
            };

            var jwtToken = new JwtSecurityToken(
				_jwtOptions.Issuer,
				_jwtOptions.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(expiration),
                new SigningCredentials(
					new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SigningKey)),
					SecurityAlgorithms.HmacSha256)
            );

            string accessToken = new JwtSecurityTokenHandler()
                .WriteToken(jwtToken);

            return new JwtTokenResult()
            {
                AccessToken = accessToken,
                Expires = expiration
			};
		}
    }
}

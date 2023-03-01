using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Booking.Web.Services.Account
{
    public class TokenService : ITokenService
    {
        private JwtOptions _jwtOptions;

        public TokenService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
		}

        public async Task<JwtTokenResult> GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(),ClaimValueTypes.UInteger64)
            };

            var jwtToken = new JwtSecurityToken(
				_jwtOptions.Issuer,
				_jwtOptions.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.Now.AddMinutes(_jwtOptions.AccessTokenExpiryInMinutes),
                new SigningCredentials(
					new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SigningKey)),
					SecurityAlgorithms.HmacSha256)
            );

            var accessToken = new JwtSecurityTokenHandler()
                .WriteToken(jwtToken);

            return new JwtTokenResult()
            {
                AccessToken = accessToken,
                AccessTokenExpires = DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenExpiryInMinutes)
            };
		}

        public async Task<RefreshToken> GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenExpiryInMinutes)
            };

            return refreshToken;
        }

        public async Task<bool> ValidateAccessToken(string tokenString)
        {
            ClaimsPrincipal principal;

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    principal = null;
                }
                else
                {
                    var symmetricKey = Encoding.ASCII.GetBytes(_jwtOptions.SigningKey);

                    var validationParameters = new TokenValidationParameters()
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                    };

                    SecurityToken securityToken;
                    principal = tokenHandler.ValidateToken(tokenString, validationParameters, out securityToken);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public async Task<JwtSecurityToken> GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SigningKey);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }

        public async Task<string> GenerateConfirmEmailToken(User user)
        {
            var expiration = DateTime.Now.AddMinutes(5);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(),ClaimValueTypes.UInteger64)
            };

            var jwtToken = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                DateTime.UtcNow,
                expiration,
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SigningKey)),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
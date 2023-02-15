using Booking.ApplicationCore.Models;
using Booking.Web.Services.Account;
using System.IdentityModel.Tokens.Jwt;

namespace Booking.Web.Interfaces.Login;

public interface ITokenService
{
    Task<JwtTokenResult> GenerateAccessToken(User user);

    Task<RefreshToken> GenerateRefreshToken();

    Task<string> GenerateConfirmEmailToken(User user);

    Task<bool> ValidateAccessToken(string tokenString);

    Task<JwtSecurityToken> GetPrincipalFromExpiredToken(string token);
}
using Booking.ApplicationCore.Models;
using Booking.Web.Services.Account;

namespace Booking.Web.Interfaces.Login;

public interface IJwtProvider
{
	JwtTokenResult Generate(User user);
}
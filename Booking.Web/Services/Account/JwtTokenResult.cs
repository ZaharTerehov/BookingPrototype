namespace Booking.Web.Services.Account
{
	public sealed class JwtTokenResult
	{
		public string AccessToken { get; internal set; }

		public DateTime AccessTokenExpires { get; set; }

		public RefreshToken RefreshToken { get; set; }
	}
}

namespace Booking.Web.Models.Account
{
    public class CaptchaConfig
    {
        public string? SiteKey { get; init; }
        public string? SecretKey { get; init; }

        private double _acceptableScore;
        public double AcceptableScore { get { return _acceptableScore; } init { Convert.ToDouble(_acceptableScore); } }
    }
}

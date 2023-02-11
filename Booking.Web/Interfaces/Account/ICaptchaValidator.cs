namespace Booking.Web.Interfaces.Account
{
    public interface ICaptchaValidator
    {
        Task<bool> IsCaptchaPassedAsync(string token);
    }
}

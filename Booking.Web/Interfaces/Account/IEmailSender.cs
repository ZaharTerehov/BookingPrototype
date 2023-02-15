namespace Booking.Web.Interfaces.Account
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string userEmail, string subject, string message);
    }
}

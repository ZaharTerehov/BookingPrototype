using Booking.Web.Interfaces.Account;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace Booking.Web.Services.Account
{
    public class EmailSenderService : IEmailSender
    {
        private readonly string _email;
        private readonly string _password;

        public EmailSenderService(IConfiguration configuration) 
        {
            _email = configuration.GetValue<string>("Email:From");
            _password = configuration.GetValue<string>("Email:Password");
        }

        public async Task SendEmailAsync(string userEmail, string subject, string message)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_email));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            var smtpServer = new SmtpClient();

            smtpServer.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtpServer.Authenticate(_email, _password);
            smtpServer.Send(email);
            smtpServer.Disconnect(true);
        }
    }
}

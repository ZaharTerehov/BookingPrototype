using Booking.ApplicationCore.Enum;

namespace Booking.ApplicationCore.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public decimal? NumberPhone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Avatar { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public Role Role { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryInMinutes { get; set; }

        public bool EmailIsVerified { get; set; } = false;

        public string EmailVerificationToken { get; set; } = string.Empty;

        public void UpdateDetails(UserDetails details)
        {
            Email = details.Email;
            Password = details.Password;
            Name = details.Name;
            Surname = details.Surname;
            NumberPhone = details.NumberPhone;
            DateOfBirth = details.DateOfBirth;
        }

        public void UpdateRefreshToken(string refreshToken, DateTime refreshTokenExpiryInMinutes)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryInMinutes = refreshTokenExpiryInMinutes;
        }

        public readonly record struct UserDetails
        {
            public string Email { get; }

            public string Password { get; }

            public string Name { get; }

            public string Surname { get; }

            public decimal NumberPhone { get; }

            public DateTime DateOfBirth { get; }

            public string RefreshToken { get; }

            public DateTime RefreshTokenExpiryInMinutes { get; }

            public UserDetails(string email, string password, string name, string surname,
                decimal numberPhone, DateTime dateOfBirth)
            {
                Email = email;
                Password = password;
                Name = name;
                Surname = surname;
                NumberPhone = numberPhone;
                DateOfBirth = dateOfBirth;
            }
        }
    }
}

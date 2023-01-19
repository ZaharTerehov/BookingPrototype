
using Booking.ApplicationCore.Enum;

namespace Booking.ApplicationCore.Models
{
    public class User : BaseModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public decimal NumberPhone { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public Role Role { get; set; }
    }
}

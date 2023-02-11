using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        [MinLength(6, ErrorMessage = "Min length 6")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        [MaxLength(20, ErrorMessage = "Max length 20")]
        [MinLength(3, ErrorMessage = "Min length 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your surname")]
        [MaxLength(20, ErrorMessage = "Max length 20")]
        [MinLength(3, ErrorMessage = "Min length 3")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        public decimal NumberPhone { get; set; }

        [Required(ErrorMessage = "Enter your date of birth")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string ReCaptcha { get; set; }
    }
}

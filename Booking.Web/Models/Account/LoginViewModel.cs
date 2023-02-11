using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Models;

public class LoginViewModel 
{
    [Required(ErrorMessage = "Enter your email")]
    [Display(Name = "Login")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Enter your password")]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    public string ReCaptcha { get; set; }
}
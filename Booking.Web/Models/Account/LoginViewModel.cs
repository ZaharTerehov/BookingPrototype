using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Models;

public class LoginViewModel 
{
    [Required(ErrorMessage = "Enter your name")]
    [MaxLength(20, ErrorMessage = "")]
    [MinLength(20, ErrorMessage = "")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Enter your password")]
    [Display(Name = "Password")]
    public string Password { get; set; }
}
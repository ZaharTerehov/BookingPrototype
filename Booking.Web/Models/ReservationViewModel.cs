using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Booking.Web.Models
{
    public class ReservationViewModel
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string ArrivalDate { get; set; } 
        [Required]
        public string DepartureDate { get; set; }

    }
}

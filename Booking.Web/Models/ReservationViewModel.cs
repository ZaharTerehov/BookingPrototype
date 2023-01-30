using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Booking.Web.Attributes.Validation;

namespace Booking.Web.Models
{
    public class ReservationViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [CurrentDate]
        public DateTime? ArrivalDate { get; set; } = new DateTime();
        [Required]
        [DepartureDate("ArrivalDate")]
        public DateTime DepartureDate { get; set; }
    }
}

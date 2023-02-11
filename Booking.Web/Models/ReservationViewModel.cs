using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Booking.Web.Attributes.Validation;

namespace Booking.Web.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [NotMapped]
        public ApartmentViewModel? ApartmentInfo { get; set; }

        [Required]
        [CurrentDate]
        public DateTime ArrivalDate { get; set; } = DateTime.Now;

        [Required]
        [DepartureDate("ArrivalDate")]  
        public DateTime DepartureDate { get; set; } = DateTime.Now;

        //To display check in/out date without time

        [NotMapped]
        public string CheckInInfo => ArrivalDate.ToString("dd/MM/yyyy");

        [NotMapped]
        public string CheckOutInfo => DepartureDate.ToString("dd/MM/yyyy");
    }
}

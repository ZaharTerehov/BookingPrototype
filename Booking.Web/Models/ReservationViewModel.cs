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

        [StringLength(50)]
        public string Room { get; set; }
        public int NumberOfPeople { get; set; }
        [Required]
        public string ArrivalDate { get; set; } 
        [Required]
        public string ArrivalTime { get; set; }
        [Required]
        public string DepartureDate { get; set; }
        [Required]
        public string DepartureTime { get; set; }
        public DateTime ArrivalDateTime => DateTime.ParseExact($"{ArrivalDate} {ArrivalTime}", "MM/dd/yyyy hh:mm tt", CultureInfo.GetCultureInfo("en-US"));

        public DateTime DepartureDateTime => DateTime.ParseExact($"{DepartureDate} {DepartureTime}", "MM/dd/yyyy hh:mm tt", CultureInfo.GetCultureInfo("en-US"));
    }
}

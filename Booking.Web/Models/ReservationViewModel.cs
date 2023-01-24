using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Models
{
    public class ReservationViewModel
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }

        [StringLength(50)]
        public string Room { get; set; }
        public int NumberOfPeople { get; set; }

        [NotMapped]
        public string ArrivalDate => ArrivalDateTime.ToString("MM/dd/yyyy");

        [NotMapped]
        public string ArrivalTime => ArrivalDateTime.ToString("hh:mm tt");

        [NotMapped]
        public string DepartureDate => DepartureDateTime.ToString("MM/dd/yyyy");

        [NotMapped]
        public string DepartureTime => DepartureDateTime.ToString("hh:mm tt");
    }
}

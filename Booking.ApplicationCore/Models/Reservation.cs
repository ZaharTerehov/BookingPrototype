using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models
{
    public class Reservation : BaseModel
    {
        [Required]
        public int ApartmentId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime ArrivalDate { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }

        //[NotMapped]
        //public string ArrivalDate => ArrivalDateTime.ToString("MM/dd/yyyy");
        //[NotMapped]
        //public string DepartureDate => DepartureDateTime.ToString("MM/dd/yyyy");

    }
}

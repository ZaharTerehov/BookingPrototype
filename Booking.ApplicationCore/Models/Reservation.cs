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
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }

        [NotMapped]
        public string ArrivalDate => ArrivalDateTime.ToString("MM/dd/yyyy");
        [NotMapped]
        public string DepartureDate => DepartureDateTime.ToString("MM/dd/yyyy");

    }
}

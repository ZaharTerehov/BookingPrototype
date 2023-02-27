using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models
{
    public class Review : BaseModel
    {
        public string VoterName { get; set; }
        [Range(1, 10)]
        public int NumStars { get; set; }
        public string Comment { get; set; }

        //For Apartment
        [Required]
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

    }
}

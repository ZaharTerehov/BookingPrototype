using Booking.ApplicationCore.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Models
{
    public class ReviewViewModel
    {
        [Key]
        public int Id { get; set; }

        public string VoterName { get; set; }
        [Range(1,10)]
        public int NumStars { get; set; }
        [Required]
        public string Comment { get; set; }

        //For Apartment
        [Required]
        public int ApartmentId { get; set; }

        public string ApartmentName { get; set; }

        public IList<ApartmentPicture>? ApartmentPictures { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Booking.ApplicationCore.Attributes.Validation;
using Booking.ApplicationCore.Models;

namespace Booking.Web.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public int ApartmentId { get; set; }

        public string? ApartmentName { get; set; }
        public string? ApartmentDescription { get; set; }
        public string? ApartmentPicture { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [CurrentDate]
        public string? ArrivalDateS { get; set; }

        [Required]
        [DepartureDate("ArrivalDateS")]  
        public string? DepartureDateS { get; set; }     
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.ApplicationCore.Models.Abstracts;

namespace Booking.ApplicationCore.Models
{
    public class Reservation : DateIntervalClass
    {
        [Required]
        public int ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }      
    }
}

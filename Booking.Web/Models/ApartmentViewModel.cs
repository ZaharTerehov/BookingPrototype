using Booking.ApplicationCore.Constants;
using Booking.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Models
{
    public sealed class ApartmentViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public string Picture { get; set; }
        public string CityName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(ApplicationConstants.MinPeopleNumber, ApplicationConstants.MaxPeopleNumber)]
        public byte PeopleNumber { get; set; } = ApplicationConstants.MinPeopleNumber;

        public IList<SelectListItem>? ApartmentTypes { get; set; }
        [Required]
        public int? ApartmentTypeFilterApplied { get; set; }

        public IList<SelectListItem>? Cities { get; set; }
        [Required]        
        public int? CityFilterApplied { get; set; }
    }
}

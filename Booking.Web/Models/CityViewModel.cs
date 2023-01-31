using Booking.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string? CountryName { get; set; }
        public IList<SelectListItem>? Countries { get; set; }
        [Required]
        public int? CountryFilterApplied { get; set; }
    }
}


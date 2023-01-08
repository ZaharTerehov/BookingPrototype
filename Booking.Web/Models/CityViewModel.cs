using Booking.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem>? Countries { get; set; }
        public int? CountryFilterApplied { get; set; }
    }
}


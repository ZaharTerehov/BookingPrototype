using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public sealed class ApartamentIndexViewModel
    {
        public List<SelectListItem>? Types { get; set; }
        public List<SelectListItem>? Country { get; set; }
        public List<SelectListItem>? City { get; set; }
        public int? TypesFilterApplied { get; set; }
        public int? CountryFilterApplied { get; set; }
        public int? CityFilterApplied { get; set; }
    }
}

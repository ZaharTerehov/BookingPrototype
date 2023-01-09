using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class CityIndexViewModel
    {
        public List<SelectListItem>? Countries { get; set; }
        public List<CityViewModel>? Cities { get; set; }
        public int? CountryFilterApplied { get; set; }
    }
}

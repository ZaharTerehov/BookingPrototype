using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class CityIndexViewModel
    {
        public IList<SelectListItem>? Countries { get; set; }
        public IList<CityViewModel>? Cities { get; set; }
        public int? CountryFilterApplied { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public sealed class ApartmentIndexViewModel
    {
        public IList<ApartmentViewModel>? Apartments { get; set; }
        public IList<SelectListItem>? ApartmentTypes { get; set; }
        public PageViewModel? PageViewModel { get; set; }                
        public int? ApartmentTypeFilterApplied { get; set; }

    }
}

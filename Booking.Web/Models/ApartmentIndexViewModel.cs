using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Services.QueryOptions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public sealed class ApartmentIndexViewModel
    {
        public IList<ApartmentViewModel>? Apartments { get; set; }
        public IList<SelectListItem>? ApartmentTypes { get; set; }
        public IList<SelectListItem>? Cities { get; set; }
        public ApartmentQueryOptions? Options { get; set; }
        //public PageOptions? PageOptions { get; set; }                
        //public int? ApartmentTypeFilterApplied { get; set; }

    }
}

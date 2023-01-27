using Booking.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public sealed class ApartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }
        public string CityName { get; set; }

        public IList<SelectListItem>? ApartmentTypes { get; set; }
        public int? ApartmentTypeFilterApplied { get; set; }

        public IList<SelectListItem>? Cities { get; set; }
        public int? CityFilterApplied { get; set; }
    }
}

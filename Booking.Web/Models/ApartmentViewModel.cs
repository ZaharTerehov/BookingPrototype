﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public sealed class ApartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public IList<SelectListItem>? ApartmentTypes { get; set; }
        public int? ApatrmentTypeFilterApplied { get; set; }
    }
}

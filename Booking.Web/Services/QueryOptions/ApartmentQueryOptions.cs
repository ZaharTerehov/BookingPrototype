using Booking.ApplicationCore.Constants;
using Booking.ApplicationCore.Enums;
using Booking.ApplicationCore.QueryOptions;
using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Services.QueryOptions
{
    public class ApartmentQueryOptions
    {
        public PageOptions PageOptions { get; set; }
        public int? ApartmentTypeFilterApplied { get; set; }
        public int? CityFilterApplied { get; set; }
        [Range(1,10)]
        public int? NeedPeopleNumber { get; set; }
        public string? SearchText { get; set; }
        public ApartmentQueryOptions()
        {
            NeedPeopleNumber = 1;
            PageOptions = new PageOptions(0, 1, ApplicationConstants.ApartmentsPageSize);
        }
    }
}

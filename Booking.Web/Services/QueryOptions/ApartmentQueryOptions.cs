using Booking.ApplicationCore.Constants;
using Booking.ApplicationCore.Enums;
using Booking.ApplicationCore.QueryOptions;

namespace Booking.Web.Services.QueryOptions
{
    public class ApartmentQueryOptions
    {
        public PageOptions PageOptions { get; set; }
        public int? ApartmentTypeFilterApplied { get; set; }
        public int? CityFilterApplied { get; set; }

        public ApartmentQueryOptions()
        {
            PageOptions = new PageOptions(0, 1, ApplicationConstants.ApartmentsPageSize);
        }
    }
}

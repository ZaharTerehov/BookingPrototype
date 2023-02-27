using Booking.ApplicationCore.Constants;
using Booking.ApplicationCore.Enums;
using Booking.ApplicationCore.Extentions;
using Booking.ApplicationCore.QueryOptions;
using Booking.ApplicationCore.Attributes.Validation;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using Booking.ApplicationCore.Models.Abstracts;

namespace Booking.Web.Services.QueryOptions
{
    public class ApartmentQueryOptions : DateIntervalClass
    {
        public PageOptions PageOptions { get; set; }
        public int? ApartmentTypeFilterApplied { get; set; }
        public int? CityFilterApplied { get; set; }
        [Range(ApplicationConstants.MinPeopleNumber, ApplicationConstants.MaxPeopleNumber)]
        public int? NeedPeopleNumber { get; set; }
        public string? SearchText { get; set; }                

        public ApartmentQueryOptions()
        {            
            NeedPeopleNumber = ApplicationConstants.MinPeopleNumber;
            PageOptions = new PageOptions(0, ApplicationConstants.FirstPage, ApplicationConstants.ApartmentsPageSize);
        }
    }
}

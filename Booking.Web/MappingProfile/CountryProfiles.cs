using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.MappingProfile
{
    public class CountryProfiles: Profile
    {
        public CountryProfiles()
        {
            CreateMap<Country, CountryViewModel>();
            CreateMap<CountryViewModel, Country>();
        }        
    }
}

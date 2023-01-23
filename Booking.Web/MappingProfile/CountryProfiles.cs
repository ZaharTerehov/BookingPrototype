using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.MappingProfile
{
    public class CountryProfiles: Profile
    {
        public CountryProfiles()
        {
            CreateMap<Country, CountryViewModel>();
            CreateMap<CountryViewModel, Country>();
            CreateMap<Country, SelectListItem>()
               .ForMember(dto => dto.Value, opt => opt.MapFrom(entity => entity.Id))
               .ForMember(dto => dto.Text, opt => opt.MapFrom(entity => entity.Name));            
        }        
    }
}

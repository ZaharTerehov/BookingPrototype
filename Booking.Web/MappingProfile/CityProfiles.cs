using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.MappingProfile
{
    public class CityProfiles : Profile
    {
        public CityProfiles() 
        {
            CreateMap<City, CityViewModel>();

            CreateMap<CityViewModel, City>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name))
                .ForMember(dto => dto.CountryId, opt => opt.MapFrom(entity => entity.CountryFilterApplied));
        }
    }
}

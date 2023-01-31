using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.MappingProfile
{
    public class CityProfiles : Profile
    {
        public CityProfiles() 
        {
            CreateMap<City, CityViewModel>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name))
                .ForMember(dto => dto.CountryFilterApplied, opt => opt.MapFrom(entity => entity.CountryId))
                .ForMember(dto => dto.CountryName, opt => opt.MapFrom(entity => entity.Country.Name));

            CreateMap<CityViewModel, City>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name))
                .ForMember(dto => dto.CountryId, opt => opt.MapFrom(entity => entity.CountryFilterApplied));

            CreateMap<City, SelectListItem>()
              .ForMember(dto => dto.Value, opt => opt.MapFrom(entity => entity.Id))
              .ForMember(dto => dto.Text, opt => opt.MapFrom(entity => entity.Name));
        }
    }
}

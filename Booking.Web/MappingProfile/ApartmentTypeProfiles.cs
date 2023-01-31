using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.MappingProfile
{
    public class ApartmentTypeProfiles : Profile
    {
        public ApartmentTypeProfiles() 
        {
            CreateMap<ApartmentType, ApartmentTypeViewModel>();
            CreateMap<ApartmentTypeViewModel, ApartmentType>();
            CreateMap<ApartmentType, SelectListItem>()
               .ForMember(dto => dto.Value, opt => opt.MapFrom(entity => entity.Id))
               .ForMember(dto => dto.Text, opt => opt.MapFrom(entity => entity.Name));
        }

    }
}

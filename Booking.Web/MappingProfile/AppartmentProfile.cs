using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.MappingProfile
{
    public class AppartmentProfile : Profile
    {
        public AppartmentProfile() 
        {
            CreateMap<ApartmentType, ApartmentTypeViewModel>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name));

            CreateMap<ApartmentTypeViewModel, ApartmentType>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Name));
        }

    }
}

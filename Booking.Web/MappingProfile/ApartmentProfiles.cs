using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.MappingProfile
{
    public class ApartmentProfiles : Profile
    {
        public ApartmentProfiles()
        {
            CreateMap<Apartment, ApartmentViewModel>()
             .ForMember(dto => dto.ApatrmentTypeFilterApplied, opt => opt.MapFrom(entity => entity.ApartmentTypeId));
            CreateMap<ApartmentViewModel, Apartment>()            
            .ForMember(dto => dto.ApartmentTypeId, opt => opt.MapFrom(entity => entity.ApatrmentTypeFilterApplied));
        }
    }
}

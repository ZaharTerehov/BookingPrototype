using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.MappingProfile
{
    public class ReservationProfiles : Profile
    {
        public ReservationProfiles()
        {
            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(dto => dto.ApartmentName, opt => opt.MapFrom(entity => entity.Apartment.Name))
                .ForMember(dto => dto.ApartmentPictures, opt => opt.MapFrom(entity => entity.Apartment.Pictures))
                .ForMember(dto => dto.ApartmentDescription, opt => opt.MapFrom(entity => entity.Apartment.Description));
            CreateMap<ReservationViewModel, Reservation>();
            CreateMap<Apartment, ReservationViewModel>()
                .ForMember(dto => dto.Id, opt => opt.Ignore())
                .ForMember(dto => dto.Name, opt => opt.Ignore())
                .ForMember(dto => dto.ApartmentId, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.ApartmentName, opt => opt.MapFrom(entity => entity.Name))
                .ForMember(dto => dto.ApartmentPictures, opt => opt.MapFrom(entity => entity.Pictures))
                .ForMember(dto => dto.Reviews, opt => opt.MapFrom(entity => entity.Reviews))
                .ForMember(dto => dto.ApartmentDescription, opt => opt.MapFrom(entity => entity.Description));
        }
    }
}

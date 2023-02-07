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
                .ForMember(dst => dst.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDateTime))
                .ForMember(dst => dst.DepartureDate, opt => opt.MapFrom(src => src.DepartureDateTime))
                .ForMember(dst=>dst.Id, opt=>opt.MapFrom(src => src.Id));

            CreateMap<ReservationViewModel, Reservation>()
                .ForMember(dst => dst.ArrivalDateTime, opt => opt.MapFrom(src => src.ArrivalDate))
                .ForMember(dst => dst.DepartureDateTime, opt => opt.MapFrom(src => src.DepartureDate))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id)); 
        }
    }
}

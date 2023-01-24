using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.MappingProfile
{
    public class ReservationProfiles : Profile
    {
        public ReservationProfiles()
        {
            CreateMap<Reservation, ReservationViewModel>();
            CreateMap<ReservationViewModel, Reservation>();
        }
    }
}

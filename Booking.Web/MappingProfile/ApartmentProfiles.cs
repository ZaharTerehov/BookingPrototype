using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.MappingProfile
{
    public class ApartmentProfiles : Profile
    {
        public ApartmentProfiles()
        {
            CreateMap<Apartment, ApartmentViewModel>();
            CreateMap<ApartmentViewModel, Apartment>();
        }
    }
}

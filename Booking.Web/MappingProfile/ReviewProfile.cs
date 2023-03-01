using AutoMapper;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.MappingProfile
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewViewModel>()
                .ForMember(dst => dst.NumStars, opt => opt.MapFrom(entity => entity.NumStars))
                .ForMember(dst => dst.Comment, opt => opt.MapFrom(entity => entity.Comment))
                .ForMember(dst => dst.VoterName, opt => opt.MapFrom(entity => entity.VoterName))
                .ForMember(dst => dst.ApartmentId, opt => opt.MapFrom(entity => entity.ApartmentId));

            CreateMap<ReviewViewModel, Review>()
                .ForMember(dst => dst.NumStars, opt => opt.MapFrom(entity => entity.NumStars))
                .ForMember(dst => dst.Comment, opt => opt.MapFrom(entity => entity.Comment))
                .ForMember(dst => dst.VoterName, opt => opt.MapFrom(entity => entity.VoterName))
                .ForMember(dst => dst.ApartmentId, opt => opt.MapFrom(entity => entity.ApartmentId));

            CreateMap<Apartment, ReviewViewModel>()
                .ForMember(dst => dst.ApartmentId, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dst => dst.ApartmentName, opt => opt.MapFrom(entity => entity.Name))
                .ForMember(dst => dst.ApartmentPictures, opt => opt.MapFrom(entity => entity.Pictures))
                .ForMember(dst => dst.Id, opt => opt.Ignore());
                

        }
    }
}

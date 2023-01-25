using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Interfaces;
using Booking.Web.Models;

namespace Booking.Web.Services
{
    public class ReservationViewModelService : IReservationViewModerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationViewModelService(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public Task CreateApartmentTypeAsync(ReservationViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        //var options = new QueryEntityOptions<ApartmentType>().AddSortOption(false, x => x.Name);
        //var entities = await _unitOfWork.ApartmentTypes.GetAllAsync(options);

        public async Task<List<ReservationViewModel>> GetReservationsAsync()
        {
            var options = new QueryEntityOptions<Reservation>().AddSortOption(false, x => x.DateCreated);
            var reservationsList = _unitOfWork.Reservations.GetAllAsync(options);
            var allReservationsList = _mapper.Map<List<ReservationViewModel>>(reservationsList);
            return allReservationsList;
        }
    }
}

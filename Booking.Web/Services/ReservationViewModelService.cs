using AutoMapper;
using Booking.ApplicationCore.Extentions;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services.QueryOptions;

namespace Booking.Web.Services
{
    public class ReservationViewModelService : IReservationViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationViewModelService(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task CreateReservationAsync(ReservationViewModel viewModel)
        {
            var dto = _mapper.Map<Reservation>(viewModel);
            await _unitOfWork.Reservations.CreateAsync(dto);
        }

        public async Task<ReservationViewModel> GetReservationByIdAsync(int id)
        {
            var existingReservation = await _unitOfWork.Reservations.GetByIdAsync(id, x => x.Apartment!);
            if (existingReservation == null)
            {
                var exception = new Exception($"Reservation with id = {id} was not found");

                throw exception;
            }

            var dto = _mapper.Map<ReservationViewModel>(existingReservation);
            return dto;
        }

        public async Task<IList<ReservationViewModel>> GetAllReservationsAsync()
        {
            var options = new QueryViewModelOption<Reservation, ReservationViewModel>()
                .AddSortOption(true, x => x.ArrivalDate!)
                .AddSelectOption(x => new ReservationViewModel
                {
                    Id = x.Id,
                    ApartmentName = x.Apartment!.Name,
                    ApartmentPictures = x.Apartment.Pictures,
                    ApartmentDescription = x.Apartment.Description,
                    Name = x.Name,
                    Email = x.Email,
                    Price = x.Price,
                    ArrivalDateS = x.ArrivalDate.ToYYYYMMDDDateFormat(),
                    DepartureDateS = x.DepartureDate.ToYYYYMMDDDateFormat()
                });
            var reservationsList = await _unitOfWork.Reservations.GetAllDtoAsync(options);
            
            return reservationsList;
        }

        public async Task DeleteApartmentAsync(int id)
        {
            var existingReservation = await _unitOfWork.Reservations.GetByIdAsync(id);
            if (existingReservation is null)
            {
                var exception = new Exception($"Reservation with id = {id} was not found");

                throw exception;
            }

            await _unitOfWork.Reservations.DeleteAsync(existingReservation);
        }

        public async Task<ReservationViewModel> GetNewReservationViewModelAsync(ApartmentReserveOptions reserveOptions)
        {
            var chosenApartment = await _unitOfWork.Apartments.GetByIdAsync(reserveOptions.ApartmentId, x => x.Pictures, t=>t.Reviews);
            var newReservation = _mapper.Map<ReservationViewModel>(chosenApartment);
            newReservation.ArrivalDateS = reserveOptions.CheckInDateS;
            newReservation.DepartureDateS = reserveOptions.CheckOutDateS;
            return newReservation;
        }
    }
}

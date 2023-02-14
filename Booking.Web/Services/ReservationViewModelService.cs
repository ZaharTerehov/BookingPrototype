using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Interfaces;
using Booking.Web.Models;

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
            var existingReservation = await _unitOfWork.Reservations.GetByIdAsync(id, x => x.Apartment);
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
                .AddSortOption(false, x => x.Name)
                .AddSelectOption(x => new ReservationViewModel
                {
                    Id = x.Id,
                    ApartmentName = x.Apartment.Name,
                    Name = x.Name,
                    Email = x.Email,
                    Price = x.Price,
                    ArrivalDate = x.ArrivalDate,
                    DepartureDate = x.DepartureDate
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

        public async Task<ReservationViewModel> GetNewReservationViewModelAsync(int apartmentId)
        {
            var chosenApartment = await _unitOfWork.Apartments.GetByIdAsync(apartmentId);
            var newReservation = _mapper.Map<ReservationViewModel>(chosenApartment);
            //var newReservation = new ReservationViewModel() 
            //    {   ApartmentId = apartmentId, 
            //        ApartmentName = chosenApartment.Name, 
            //        ApartmentDescription = chosenApartment.Description,
            //        ApartmentPicture = chosenApartment.Picture,
            //        Price = chosenApartment.Price };
            return newReservation;
        }
    }
}

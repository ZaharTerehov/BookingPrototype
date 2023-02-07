﻿using AutoMapper;
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

        public async Task CreateReservationAsync(ReservationViewModel viewModel)
        {
            var dto = _mapper.Map<Reservation>(viewModel);
            await _unitOfWork.Reservations.CreateAsync(dto);
        }

        public async Task<ReservationViewModel> GetReservationByIdAsync(int id)
        {
            var existingReservation = await _unitOfWork.Reservations.GetByIdAsync(id);
            if (existingReservation == null)
            {
                var exception = new Exception($"Reservation with id = {id} was not found");

                throw exception;
            }

            var dto = _mapper.Map<ReservationViewModel>(existingReservation);
            return dto;
        }

        //var options = new QueryEntityOptions<ApartmentType>().AddSortOption(false, x => x.Name);
        //var entities = await _unitOfWork.ApartmentTypes.GetAllAsync(options);

        public async Task<List<ReservationViewModel>> GetAllReservationsAsync()
        {
            var options = new QueryEntityOptions<Reservation>().AddSortOption(false, x => x.Name);
            var reservationsList = await _unitOfWork.Reservations.GetAllAsync(options);
            var allReservationsList = _mapper.Map<List<ReservationViewModel>>(reservationsList);
            return allReservationsList;
        }

        public async Task DeleteApartmentAsync(ReservationViewModel viewModel)
        {
            var existingReservation = await _unitOfWork.Reservations.GetByIdAsync(viewModel.Id);
            if (existingReservation is null)
            {
                var exception = new Exception($"Reservation with id = {viewModel.Id} was not found");

                throw exception;
            }

            await _unitOfWork.Reservations.DeleteAsync(existingReservation);
        } 
    }
}

using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Extentions;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using System.Diagnostics.Metrics;

namespace Booking.Web.Services
{
    public sealed class ApartmentTypeViewModelService : IApartmentTypeViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApartmentTypeViewModelService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<ApartmentTypeViewModel> GetApartmentTypeViewModelByIdAsync(int id)
        {
            var apartmentType = await _unitOfWork.ApartmentTypes.GetByIdAsync(id);
            if (apartmentType == null)
            {
                var exception = new Exception($"Apartment type with id = {id} was not found");

                throw exception;
            }

            var dto = _mapper.Map<ApartmentTypeViewModel>(apartmentType);

            return dto;
        }

        public async Task<List<ApartmentTypeViewModel>> GetApartmentTypesAsync()
        {
            var options = new QueryEntityOptions<ApartmentType>().AddSortOption(false, x => x.Name);
            var entities = await _unitOfWork.ApartmentTypes.GetAllAsync(options);
            var apartmentTypes = _mapper.Map<List<ApartmentTypeViewModel>>(entities);

            return apartmentTypes;
        }       

        public async Task UpdateApartmentType(ApartmentTypeViewModel viewModel)
        {
            var existingApartmentType = await _unitOfWork.ApartmentTypes.GetByIdAsync(viewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Apartment type {viewModel.Id} was not found");
                throw exception;
            }

            ApartmentType.ApartmentTypeDetails details = new ApartmentType.ApartmentTypeDetails(viewModel.Name);
            existingApartmentType.UpdateDetails(details);
            await _unitOfWork.ApartmentTypes.UpdateAsync(existingApartmentType);
        }

        public async Task CreateApartmentTypeAsync(ApartmentTypeViewModel viewModel)
        {
            var dto = _mapper.Map<ApartmentType>(viewModel);
            await _unitOfWork.ApartmentTypes.CreateAsync(dto);
        }

        public async Task DeleteApartmentTypeAsync(ApartmentTypeViewModel viewModel)
        {
            var existingApartmentType = await _unitOfWork.ApartmentTypes.GetByIdAsync(viewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Apartment type {viewModel.Id} was not found");

                throw exception;
            }

            await _unitOfWork.ApartmentTypes.DeleteAsync(existingApartmentType);
        }        
    }
}

using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Interfaces;
using Booking.Web.Models;

namespace Booking.Web.Services
{
    public sealed class ApartmentViewModelService : IApartmentViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApartmentViewModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
                _unitOfWork= unitOfWork;
            _mapper= mapper;
        }

        public async Task CreateApartmentAsync(ApartmentViewModel viewModel)
        {
            var dto = _mapper.Map<Apartment>(viewModel);
            await _unitOfWork.Apartments.CreateAsync(dto);
        }

        public async Task DeleteApartmentAsync(ApartmentViewModel viewModel)
        {
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(viewModel.Id);
            if (existingApartment is null)
            {
                var exception = new Exception($"Apartment {viewModel.Id} was not found");

                throw exception;
            }

            await _unitOfWork.Apartments.DeleteAsync(existingApartment);
        }

        public async Task<List<ApartmentViewModel>> GetAllAsync()
        {
            var options = new QueryOptions<Apartment>();
            options.AddSortOption(false, y => y.Id);
            var entities = await _unitOfWork.Apartments.GetAllAsync(options);
            var apartment = _mapper.Map<List<ApartmentViewModel>>(entities);
            return apartment;
        }

        public async Task<ApartmentViewModel> GetApartmentViewModelByIdAsync(int id)
        {
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(id);
            if (existingApartment == null)
            {
                var exception = new Exception($"Apartment with id = {id} was not found");

                throw exception;
            }

            var dto = _mapper.Map<ApartmentViewModel>(existingApartment);

            return dto;
        }

        public async Task UpdateApartmentAsync(ApartmentViewModel viewModel)
        {
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(viewModel.Id);
            if (existingApartment is null)
            {
                var exception = new Exception($"Apartment {viewModel.Id} was not found");
                throw exception;
            }

            Apartment.ApartmentDetails details = new Apartment.ApartmentDetails(viewModel.Name, viewModel.Description,viewModel.Price, viewModel.Picture);
            existingApartment.UpdateDetails(details);
            await _unitOfWork.Apartments.UpdateAsync(existingApartment);
        }
    }
}

using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Serilog;

namespace Booking.Web.Services
{
    public sealed class ApartmentTypeViewModelService : IApartmentTypeViewModelService
    {
        private readonly IRepository<ApartmentType> _apartmentTypeRepository;
        private readonly IMapper _mapper;

        public ApartmentTypeViewModelService(IMapper mapper,IRepository<ApartmentType> apartmentTypeRepostitory)
        {
            _apartmentTypeRepository = apartmentTypeRepostitory;
            _mapper = mapper;
        }

        public async Task UpdateApartmentType(ApartmentTypeViewModel viewModel)
        {
            var existingApartmentType = await _apartmentTypeRepository.GetByIdAsync(viewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Apartment type {viewModel.Id} was not found");
                throw exception;
            }

            ApartmentType.ApartmentTypeDetails details = new ApartmentType.ApartmentTypeDetails(viewModel.Name);
            existingApartmentType.UpdateDetails(details);
            await _apartmentTypeRepository.UpdateAsync(existingApartmentType);
        }

        public async Task CreateNewApartmentTypeAsync(ApartmentTypeViewModel viewModel)
        {
            var dto = _mapper.Map<ApartmentType>(viewModel);
            await _apartmentTypeRepository.CreateAsync(dto);
        }

        public async Task DeleteApartmentTypeAsync(ApartmentTypeViewModel apartmentTypeViewModel)
        {
            var existingApartmentType = await _apartmentTypeRepository.GetByIdAsync(apartmentTypeViewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Apartment type {apartmentTypeViewModel.Id} was not found");

                throw exception;
            }

            await _apartmentTypeRepository.DeleteAsync(existingApartmentType);
        }

        public async Task<List<ApartmentTypeViewModel>> GetApartmentTypesAsync()
        {
            var entities = await _apartmentTypeRepository.GetAllAsync();
            var apartmentTypes = entities
                .Select(x => new ApartmentTypeViewModel
                {
                    Id= x.Id,
                    Name= x.Name
                }).ToList();

            return apartmentTypes;
        }

        public async Task<ApartmentTypeViewModel> GetApartmentTypeViewModelByIdAsync(int id)
        {
            var apartmentType = await _apartmentTypeRepository.GetByIdAsync(id);
            if (apartmentType == null)
            {
                return null;
            }

            var dto = _mapper.Map<ApartmentTypeViewModel>(apartmentType);            

            return dto;
        }
    }
}

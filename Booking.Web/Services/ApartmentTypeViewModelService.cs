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

        public void UpdateApartmentType(ApartmentTypeViewModel viewModel)
        {
            var existingApartmentType = _apartmentTypeRepository.GetById(viewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Apartment type {viewModel.Id} was not found");

                throw exception;
            }

            ApartmentType.ApartmentTypeDetails details = new ApartmentType.ApartmentTypeDetails(viewModel.Name);
            existingApartmentType.UpdateDetails(details);
            _apartmentTypeRepository.Update(existingApartmentType);
        }

        public void CreateNewApartmentType(ApartmentTypeViewModel viewModel)
        {

            var dto = _mapper.Map<ApartmentType>(viewModel);
            _apartmentTypeRepository.CreateAsync(dto);


            //var existingApartmentType = _apartmentTypeRepository.GetAll().ToList();
            //if (existingApartmentType.Any(x => x.Name == viewModel.Name))
            //{
            //    var exception = new Exception($"Apartment type {viewModel.Name} is already created");

            //    throw exception;
            //}

            //int newID = existingApartmentType.Max(x => x.Id) + 1;

            //_apartmentTypeRepository.CreateAsync(new ApartmentType() { Id = newID, Name = viewModel.Name });
        }

        public void DeleteApartmentType(ApartmentTypeViewModel apartmentTypeViewModel)
        {
            var existingApartmentType = _apartmentTypeRepository.GetById(apartmentTypeViewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Apartment type {apartmentTypeViewModel.Id} was not found");

                throw exception;
            }

            _apartmentTypeRepository.DeleteAsync(existingApartmentType);
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
    }
}

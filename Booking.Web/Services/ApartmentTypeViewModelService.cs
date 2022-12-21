using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces;
using Booking.Web.Models;

namespace Booking.Web.Services
{
    public sealed class ApartmentTypeViewModelService : IApartmentTypeViewModelService
    {
        private readonly IRepository<ApartmentType> _apartmentTypeRepository;

        public ApartmentTypeViewModelService(IRepository<ApartmentType> apartmentTypeRepostitory)
        {
            _apartmentTypeRepository = apartmentTypeRepostitory;
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
            var existingApartmentType = _apartmentTypeRepository.GetAll().ToList();
            if (existingApartmentType.Any(x => x.Name == viewModel.Name))
            {
                var exception = new Exception($"Apartment type {viewModel.Name} is already created");

                throw exception;
            }

            int newID = existingApartmentType.Max(x => x.Id) + 1;

            _apartmentTypeRepository.Create(new ApartmentType() { Id = newID, Name = viewModel.Name });
        }
    }
}

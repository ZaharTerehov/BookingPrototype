using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces;

namespace Booking.Web.Services
{
    public sealed class ApartmentTypeViewModelService : IApartmentTypeViewModelService
    {
        private readonly IRepository<ApartmentType> _apartmentTypeRepository;

        public ApartmentTypeViewModelService(IRepository<ApartmentType> apartmentTypeRepostitory)
        {
            _apartmentTypeRepository = apartmentTypeRepostitory;
        }

        public void UpdateApartmentType(ApartmentType viewModel)
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
    }
}

using Booking.ApplicationCore.Models;

namespace Booking.Web.Interfaces
{
    public interface IApartmentTypeViewModelService
    {
        void UpdateApartmentType(ApartmentType apartmentTypeViewModel);
        void CreateNewApartmentType(ApartmentType apartmentTypeViewModel);
    }
}

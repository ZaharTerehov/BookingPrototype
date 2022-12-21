using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IApartmentTypeViewModelService
    {
        void UpdateApartmentType(ApartmentTypeViewModel apartmentTypeViewModel);
        void CreateNewApartmentType(ApartmentTypeViewModel apartmentTypeViewModel);
        void DeleteApartmentType(ApartmentTypeViewModel apartmentTypeViewModel);
    }
}

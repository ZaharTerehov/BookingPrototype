using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IApartmentTypeViewModelService
    {
        Task UpdateApartmentType(ApartmentTypeViewModel apartmentTypeViewModel);
        Task CreateNewApartmentTypeAsync(ApartmentTypeViewModel apartmentTypeViewModel);
        Task DeleteApartmentTypeAsync(ApartmentTypeViewModel apartmentTypeViewModel);

        Task<List<ApartmentTypeViewModel>> GetApartmentTypesAsync();
        Task<ApartmentTypeViewModel> GetApartmentTypeViewModelByIdAsync(int id);
    }
}

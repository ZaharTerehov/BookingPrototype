using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IApartmentTypeViewModelService
    {
        Task UpdateApartmentType(ApartmentTypeViewModel viewModel);
        Task CreateApartmentTypeAsync(ApartmentTypeViewModel viewModel);
        Task DeleteApartmentTypeAsync(ApartmentTypeViewModel viewModel);
        Task<List<ApartmentTypeViewModel>> GetApartmentTypesAsync();
        Task<ApartmentTypeViewModel> GetApartmentTypeViewModelByIdAsync(int id);
    }
}

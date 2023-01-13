using Booking.ApplicationCore.Models;
using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IApartmentViewModelService
    {
        Task<List<ApartmentViewModel>> GetAllAsync();

        Task CreateApartmentAsync(ApartmentViewModel viewModel);
        Task UpdateApartmentAsync(ApartmentViewModel viewModel);
        Task DeleteApartmentAsync(ApartmentViewModel viewModel);
        Task<ApartmentViewModel> GetApartmentViewModelByIdAsync(int id);

    }
}

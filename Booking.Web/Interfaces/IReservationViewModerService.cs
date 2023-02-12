using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IReservationViewModerService
    {
        Task CreateReservationAsync(ReservationViewModel viewModel);
        Task DeleteApartmentAsync(int id);
        Task<List<ReservationCreateViewModel>> GetAllReservationsAsync();
        Task<ReservationCreateViewModel> GetReservationByIdAsync(int id);

    }
}

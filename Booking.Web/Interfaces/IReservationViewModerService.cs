using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IReservationViewModerService
    {
        Task CreateReservationAsync(ReservationViewModel viewModel);
        Task DeleteApartmentAsync(ReservationViewModel viewModel);
        Task<List<ReservationViewModel>> GetAllReservationsAsync();
        Task<ReservationViewModel> GetReservationByIdAsync(int id);

    }
}

using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IReservationViewModelService
    {
        Task CreateReservationAsync(ReservationViewModel viewModel);
        Task DeleteApartmentAsync(int id);
        Task<IList<ReservationViewModel>> GetAllReservationsAsync();
        Task<ReservationViewModel> GetReservationByIdAsync(int id);
        Task<ReservationViewModel> GetNewReservationViewModelAsync(int apartmentId);

    }
}

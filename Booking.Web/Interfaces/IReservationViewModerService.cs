using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IReservationViewModerService
    {
        Task CreateApartmentTypeAsync(ReservationViewModel viewModel);
        Task<List<ReservationViewModel>> GetReservationsAsync();
    }
}

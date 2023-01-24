using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IReservationViewModerService
    {
        Task<List<ReservationViewModel>> GetReservationsAsync();
    }
}

using Booking.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationViewModerService _reservationViewModelService;

        public ReservationController(IReservationViewModerService reservationViewModelService)
        {
            _reservationViewModelService = reservationViewModelService;
        }

        public async Task<IActionResult> Index()
        {
            var reservationsList = await _reservationViewModelService.GetReservationsAsync();
            return View(reservationsList);
        }
    }
}

using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services;
using Booking.Web.Services.QueryOptions;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationViewModelService _reservationViewModelService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReservationController> _logger;
        private readonly IMapper _mapper;

        public ReservationController(IMapper mapper,
            IReservationViewModelService reservationViewModelService,
            ILogger<ReservationController> logger,
            IUnitOfWork unitOfWork)
        {
            _reservationViewModelService = reservationViewModelService;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var reservationsList = await _reservationViewModelService.GetAllReservationsAsync();
            return View(reservationsList);
        }

        [HttpGet]
        public async Task<IActionResult> Create(ApartmentReserveOptions reserveOptions)
        {
            var newReservation = await _reservationViewModelService.GetNewReservationViewModelAsync(reserveOptions);

            return View(newReservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _reservationViewModelService.CreateReservationAsync(viewModel);
                return RedirectToAction("Index", "Reservation");
            }
            else
            {
                _logger.LogInformation("Model for member {Name} was incorrect",  viewModel.Name);
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reservationViewModelService.GetReservationByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            #region Validation
            if (ModelState.IsValid)
            {
                await _reservationViewModelService.DeleteApartmentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
            #endregion
        }
    }
}

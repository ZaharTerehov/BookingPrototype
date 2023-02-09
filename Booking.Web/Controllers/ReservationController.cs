using AutoMapper;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationViewModerService _reservationViewModelService;
        private readonly IApartmentViewModelService _apartmentViewModelService;
        private readonly ILogger<ReservationController> _logger;
        private readonly IMapper _mapper;

        public ReservationController(IMapper mapper,
            IReservationViewModerService reservationViewModelService,
            ILogger<ReservationController> logger, 
            IApartmentViewModelService apartmentViewModelService)
        {
            _reservationViewModelService = reservationViewModelService;
            _logger = logger;
            _mapper = mapper;
            _apartmentViewModelService=apartmentViewModelService;
        }

        public async Task<IActionResult> Index()
        {
            var reservationsList = await _reservationViewModelService.GetReservationsAsync();
            return View(reservationsList);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync(int id)
        {
            var chosenApartment = await _apartmentViewModelService.GetApartmentViewModelByIdAsync(id);
            var newReservation = new ReservationViewModel();

            var reservationCreateViewModel = new ReservationCreateViewModel
            {
                ApartmentViewModel = chosenApartment,
                ReservationViewModel = newReservation
            };

            return View(reservationCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var reservation = _mapper.Map<ReservationViewModel>(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
            //try
            //{
            //    var reservation = _mapper.Map<ReservationViewModel>(viewModel);
            //    await _reservationViewModelService.CreateReservationAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message, ex);
            //    throw View();
            //}
        }
    }
}

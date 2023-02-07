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
            var reservationsList = await _reservationViewModelService.GetAllReservationsAsync();
            return View(reservationsList);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync(int id)
        {
            var chosenApartment = await _apartmentViewModelService.GetApartmentViewModelByIdAsync(id);
            ViewBag.ApartmentInfo = chosenApartment;
            var newReservation = new ReservationViewModel();

            //var reservationCreateViewModel = new ReservationCreateViewModel
            //{
            //    ApartmentViewModel = chosenApartment,
            //    ReservationViewModel = newReservation
            //};

            //return View(reservationCreateViewModel);

            return View(newReservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //var reservation = _mapper.Map<ReservationViewModel>(viewModel.ReservationViewModel);
                await _reservationViewModelService.CreateReservationAsync(viewModel);
                return RedirectToAction("Index", "Apartment");
            }
            else
            {
                _logger.LogInformation("Model for member {Name} was incorrect",  viewModel.Name);
                return View(viewModel);
            }
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
        public async Task<IActionResult> Delete(ReservationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _reservationViewModelService.DeleteApartmentAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}

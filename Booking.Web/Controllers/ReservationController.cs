using AutoMapper;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationViewModerService _reservationViewModelService;
        private readonly ILogger<ReservationController> _logger;
        private readonly IMapper _mapper;

        public ReservationController(IMapper mapper,
            IReservationViewModerService reservationViewModelService,
            ILogger<ReservationController> logger)
        {
            _reservationViewModelService = reservationViewModelService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var reservationsList = await _reservationViewModelService.GetReservationsAsync();
            return View(reservationsList);
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            
            return View(new ReservationViewModel());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ReservationViewModel viewModel) 
        //{
        //    try
        //    {
        //        var reservation = _mapper.Map<ReservationViewModel>(viewModel);
        //        await _reservationViewModelService.CreateReservationAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message,ex);
        //        throw View();
        //    }
        //}
    }
}

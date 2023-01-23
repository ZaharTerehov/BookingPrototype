using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ApartmentTypeController : Controller
    {
        private readonly IApartmentTypeViewModelService _apartmentTypeViewModelService;        
        private readonly ILogger<ApartmentTypeController> _logger;
        private readonly IMapper _mapper;

        public ApartmentTypeController(IMapper mapper,
            IApartmentTypeViewModelService apartmentTypeViewModelService,
            ILogger<ApartmentTypeController> logger)
        {
            _apartmentTypeViewModelService = apartmentTypeViewModelService;            
            _logger=logger;
            _mapper = mapper;
        }

        public async Task <IActionResult> Index()
        {
            var apartmentsViewModel = await _apartmentTypeViewModelService.GetApartmentTypesAsync();
            return View(apartmentsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var result = await _apartmentTypeViewModelService.GetApartmentTypeViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApartmentTypeViewModel viewModel)
        {
            try
            {
                await _apartmentTypeViewModelService.UpdateApartmentType(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ApartmentTypeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApartmentTypeViewModel viewModel)
        {
            try
            {
                var apartment = _mapper.Map<ApartmentTypeViewModel>(viewModel);
                await _apartmentTypeViewModelService.CreateApartmentTypeAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
               return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _apartmentTypeViewModelService.GetApartmentTypeViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }            

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ApartmentTypeViewModel viewModel)
        {
            try
            {
                await _apartmentTypeViewModelService.DeleteApartmentTypeAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

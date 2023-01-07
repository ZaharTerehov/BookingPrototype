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
        private readonly IRepository<ApartmentType> _apartmentTypeRepository;
        private readonly ILogger<ApartmentTypeController> _logger;
        private readonly IMapper _mapper;

        public ApartmentTypeController(IMapper mapper,IRepository<ApartmentType> apartmentTypeRepository,
            IApartmentTypeViewModelService apartmentTypeViewModelService,
            ILogger<ApartmentTypeController> logger)
        {
            _apartmentTypeViewModelService = apartmentTypeViewModelService;
            _apartmentTypeRepository = apartmentTypeRepository;
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
        public async Task<IActionResult> Edit(ApartmentTypeViewModel apartmentTypeViewModel)
        {
            try
            {
                await _apartmentTypeViewModelService.UpdateApartmentType(apartmentTypeViewModel);
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
        public async Task<IActionResult> Create(ApartmentTypeViewModel apartmentTypeViewModel)
        {
            try
            {
                var apartment = _mapper.Map<ApartmentTypeViewModel>(apartmentTypeViewModel);
                await _apartmentTypeViewModelService.CreateNewApartmentTypeAsync(apartmentTypeViewModel);
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
        public async Task<IActionResult> Delete(ApartmentTypeViewModel apartmentTypeViewModel)
        {
            try
            {
                await _apartmentTypeViewModelService.DeleteApartmentTypeAsync(apartmentTypeViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

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
        public IActionResult Edit(int id) 
        {
            var apartment = _apartmentTypeRepository.GetById(id);
            if (apartment == null)
            {
                return RedirectToAction("Index");
            }

            var result = new ApartmentTypeViewModel()
            {
                Id = apartment.Id,
                Name = apartment.Name,
            };


            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApartmentTypeViewModel apartmentTypeViewModel)
        {
            try
            {
                _apartmentTypeViewModelService.UpdateApartmentType(apartmentTypeViewModel);
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
        public IActionResult Create(ApartmentTypeViewModel apartmentTypeViewModel)
        {
            //try
            //{
                var apartment = _mapper.Map<ApartmentTypeViewModel>(apartmentTypeViewModel);
                _apartmentTypeViewModelService.CreateNewApartmentType(apartment);
                return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //   return View();
            //}
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var apartment = _apartmentTypeRepository.GetById(id);
            if (apartment == null)
            {
                return RedirectToAction("Index");
            }

            var result = new ApartmentTypeViewModel()
            {
                Id = apartment.Id,
                Name = apartment.Name,
            };

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ApartmentTypeViewModel apartmentTypeViewModel)
        {
            try
            {
                _apartmentTypeViewModelService.DeleteApartmentType(apartmentTypeViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.Services;
using Booking.Web.Interfaces;
using Booking.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ApartmentTypeController : Controller
    {
        private readonly IApartmentTypeViewModelService _apartmentTypeViewModelService;
        private readonly IRepository<ApartmentType> _apartmentTypeRepository;

        public ApartmentTypeController(IRepository<ApartmentType> apartmentTypeRepository, 
            IApartmentTypeViewModelService apartmentTypeViewModelService)
        {
            _apartmentTypeViewModelService = apartmentTypeViewModelService;
            _apartmentTypeRepository =apartmentTypeRepository;
        }

        public IActionResult Index()
        {
            var apartmentsViewModel = _apartmentTypeRepository.GetAll().ToList();

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

            var result = new ApartmentType()
            {
                Id = apartment.Id,
                Name = apartment.Name,
            };
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApartmentType apartmentTypeViewModel)
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
            return View(new ApartmentType());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApartmentType apartmentTypeViewModel)
        {
            try
            {
                _apartmentTypeViewModelService.CreateNewApartmentType(apartmentTypeViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

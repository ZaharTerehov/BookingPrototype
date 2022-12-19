using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.Services;
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

        public ApartmentTypeController(IRepository<ApartmentType> apartmentTypeRepository, 
            IApartmentTypeViewModelService apartmentTypeViewModelService)
        {
            _apartmentTypeViewModelService = apartmentTypeViewModelService;
            _apartmentTypeRepository =apartmentTypeRepository;
        }

        public IActionResult Index()
        {
            var apartmentsViewModel = _apartmentTypeRepository.GetAll().Select(apartment => 
            new ApartmentTypeViewModel() { Name = apartment.Name}).ToList();

            //var catalogitemsViewModel = _catalogRepository.GetAll().Select(item => new CatalogItemViewModel()
            //{
            //    Id= item.Id,
            //    Name= item.Name,
            //    PictureUrl= item.PictureUrl,
            //    Price= item.Price,
            //}).ToList();

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
        public IActionResult Edit(ApartmentType catalogItemViewModel)
        {
            try
            {
                _apartmentTypeViewModelService.UpdateApartmentType(catalogItemViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

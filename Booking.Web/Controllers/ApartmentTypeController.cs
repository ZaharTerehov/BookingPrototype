using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ApartmentTypeController : Controller
    {
        //private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly IRepository<ApartmentType> _apartmentTypeRepository;

        public ApartmentTypeController(IRepository<ApartmentType> apartmentTypeRepository)
        {
            _apartmentTypeRepository=apartmentTypeRepository;
        }

        public IActionResult Index()
        {
            var apartmentsViewModel = _apartmentTypeRepository.GetAll().ToList();

            return View(apartmentsViewModel);
        }
    }
}

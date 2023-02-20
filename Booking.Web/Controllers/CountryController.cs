using AutoMapper;
using Booking.ApplicationCore.Attributes.Filters;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    [TypeFilter(typeof(AppExceptionFilter))]
    public class CountryController : Controller
    {
        private readonly ICountryViewModelService _countryViewModelService;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IMapper mapper,
            ICountryViewModelService countryViewModelService,
            ILogger<CountryController> logger)
        {
            _countryViewModelService = countryViewModelService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var countryViewModels = await _countryViewModelService.GetCountriesAsync();
            return View(countryViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _countryViewModelService.GetCountryViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CountryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _countryViewModelService.UpdateCountry(viewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CountryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var apartment = _mapper.Map<CountryViewModel>(viewModel);
                await _countryViewModelService.CreateCountryAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _countryViewModelService.GetCountryViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CountryViewModel viewModel)
        {
            await _countryViewModelService.DeleteCountryAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}

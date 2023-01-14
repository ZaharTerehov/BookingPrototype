using AutoMapper;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
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
            try
            {
                await _countryViewModelService.UpdateCountry(viewModel);
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
            return View(new CountryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel viewModel)
        {
            try
            {
                var apartment = _mapper.Map<CountryViewModel>(viewModel);
                await _countryViewModelService.CreateCountryAsync(viewModel);
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
            try
            {
                await _countryViewModelService.DeleteCountryAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

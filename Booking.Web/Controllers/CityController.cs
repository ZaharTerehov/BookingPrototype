﻿using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Controllers
{
    public class CityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CityController> _logger;
        private readonly ICityViewModelService _cityViewModelService;
        private readonly IUnitOfWork _unitOfWork;

        public CityController(ICityViewModelService cityViewModelService, ILogger<CityController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _cityViewModelService= cityViewModelService;
        }

        public async Task<IActionResult> Index(CityIndexViewModel model)
        {
            var viewModal = await _cityViewModelService.GetCitiesAsync(model.CountryFilterApplied);

            return View(viewModal);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var newCity = new CityViewModel();
            newCity.Countries = (List<SelectListItem>?)await _cityViewModelService.GetCountries(false);
            return View( newCity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityViewModel viewModel)
        {
            try
            {
                var city = _mapper.Map<City>(viewModel);
                await _unitOfWork.Cities.CreateAsync(city);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View();
            }
        }
    }
}

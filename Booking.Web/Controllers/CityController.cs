﻿using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Web.Extentions;
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
                await _cityViewModelService.CreateCityAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _cityViewModelService.GetCityViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            result.Countries = (await _cityViewModelService.GetCountries(false)).ToList().SetSelectedValue(result.CountryFilterApplied);
            
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityViewModel viewModel)
        {
            try
            {
                await _cityViewModelService.UpdateCity(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cityViewModelService.GetCityViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CityViewModel viewModel)
        {
            try
            {
                await _cityViewModelService.DeleteCityAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

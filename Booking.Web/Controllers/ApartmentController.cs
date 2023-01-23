using AutoMapper;
using Booking.ApplicationCore.Constants;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services;
using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Booking.Web.Extentions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Services.QueryOptions;
using Microsoft.Extensions.Options;
using Booking.ApplicationCore.Models;

namespace Booking.Web.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentViewModelService _apartmentViewModelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ApartmentController(IMapper mapper, IApartmentViewModelService apartmentViewModelService, 
            ILogger<ApartmentController> logger)
        {
            _apartmentViewModelService= apartmentViewModelService;
            _mapper=mapper;
            _logger=logger;
        }
        public async Task<IActionResult> Index(ApartmentQueryOptions options/*, int  page = 1*/)
        {            
            //options = options ?? new ApartmentQueryOptions();
            //options.PageOptions.CurrentPage = page;
            var apartmentViewModels = await _apartmentViewModelService.GetApartmentsAsync(options);
            options.PageOptions.CurrentElementsCount = apartmentViewModels.Count;
            ApartmentIndexViewModel viewModel = new ApartmentIndexViewModel()
            {
                Options = options,
                ApartmentTypes = (await _apartmentViewModelService
                                                    .GetApartmentTypes(true, options.ApartmentTypeFilterApplied == null))
                                                    .SetSelectedValue(options.ApartmentTypeFilterApplied),
                Apartments = apartmentViewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var newApartment = new ApartmentViewModel();
            newApartment.ApartmentTypes = (List<SelectListItem>?)await _apartmentViewModelService.GetApartmentTypes(false);
            return View(newApartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApartmentViewModel viewModel)
        {
            try
            {
                await _apartmentViewModelService.CreateApartmentAsync(viewModel);
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
            var result = await _apartmentViewModelService.GetApartmentViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ApartmentViewModel viewModel)
        {
            try
            {
                await _apartmentViewModelService.DeleteApartmentAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _apartmentViewModelService.GetApartmentViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }
            result.ApartmentTypes = (await _apartmentViewModelService.GetApartmentTypes(false)).SetSelectedValue(result.ApartmentTypeFilterApplied);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApartmentViewModel viewModel)
        {
            try
            {
                await _apartmentViewModelService.UpdateApartmentAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

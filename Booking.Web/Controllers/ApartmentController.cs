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
using Booking.ApplicationCore.Attributes.Filters;
using Booking.ApplicationCore.Extentions;

namespace Booking.Web.Controllers
{
    [TypeFilter(typeof(AppExceptionFilter))]
    public class ApartmentController : Controller
    {
        private readonly IApartmentViewModelService _apartmentViewModelService;        
        private readonly ILogger _logger;
        private readonly IFileService _fileService;
        public ApartmentController(
            IApartmentViewModelService apartmentViewModelService, 
            ILogger<ApartmentController> logger,
            IFileService fileService)
        {
            _apartmentViewModelService = apartmentViewModelService;
            _logger = logger;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index(ApartmentQueryOptions options)
        {
            var apartmentViewModels = await _apartmentViewModelService.GetApartmentsAsync(options);
            options.PageOptions.CurrentElementsCount = apartmentViewModels.Count;
            ApartmentIndexViewModel viewModel = new ApartmentIndexViewModel()
            {
                Options = options,
                ApartmentTypes = (await _apartmentViewModelService.GetApartmentTypes(true, options.ApartmentTypeFilterApplied == null))
                                                                  .SetSelectedValue(options.ApartmentTypeFilterApplied),
                Cities = (await _apartmentViewModelService.GetCities(true, options.CityFilterApplied == null))
                                                          .SetSelectedValue(options.CityFilterApplied),
                Apartments = apartmentViewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var newApartment = new ApartmentViewModel();
            newApartment.ApartmentTypes = await _apartmentViewModelService.GetApartmentTypes(false);
            newApartment.Cities = await _apartmentViewModelService.GetCities(false);
            return View(newApartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApartmentViewModel viewModel)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                _fileService.UploadFiles(files, ApplicationConstants.ImagesDir);
                viewModel.Pictures = _fileService.Files.ToListApartmentPictures();//.First();
            }

            if (ModelState.IsValid)
            {
                await _apartmentViewModelService.CreateApartmentAsync(viewModel);
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
            var result = await _apartmentViewModelService.GetApartmentViewModelByIdAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            if (ModelState.IsValid)
            {
                var _apartment = await _apartmentViewModelService.GetApartmentViewModelByIdAsync(id);  
                
                _fileService.DeleteFile(_apartment.Pictures.ToListStringPaths());                
                
                await _apartmentViewModelService.DeleteApartmentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            else
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
            result.Cities = (await _apartmentViewModelService.GetCities(false)).SetSelectedValue(result.CityFilterApplied);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApartmentViewModel viewModel, params string[] oldPictureUrls)
        {
            ModelState.Remove("Pictures");
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                _fileService.DeleteFile(oldPictureUrls);
                _fileService.UploadFiles(files!, ApplicationConstants.ImagesDir);
                viewModel.Pictures = _fileService.Files.ToListApartmentPictures();
            }

            if (ModelState.IsValid)
            {
                await _apartmentViewModelService.UpdateApartmentAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}

using AutoMapper;
using Booking.ApplicationCore.Constants;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services;
using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace Booking.Web.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentViewModelService _apartmentViewModelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ApartmentController(IMapper mapper, IApartmentViewModelService apartmentViewModelService, ILogger<ApartmentController> logger)
        {
            _apartmentViewModelService= apartmentViewModelService;
            _mapper=mapper;
            _logger=logger;
        }
        public async Task<IActionResult> Index(int  page = 1)
        {
            var pNS = new {PageNum = page, PageSize = ApplicationConstants.ApartmentsPageSize };

            var apartmentsViewModel = await _apartmentViewModelService.GetApartmentsAsync(pNS.PageNum, pNS.PageSize);

            ApartmentIndexViewModel viewModel = new ApartmentIndexViewModel
            {
                PageViewModel = new PageViewModel(apartmentsViewModel.Count(), pNS.PageNum, pNS.PageSize),
                Apartments = apartmentsViewModel
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new ApartmentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApartmentViewModel viewModel)
        {
            try
            {
                var apartment = _mapper.Map<ApartmentViewModel>(viewModel);
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

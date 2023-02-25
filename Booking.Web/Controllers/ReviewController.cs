using AspNetCore;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewViewModelService _reviewViewModelService;
        private readonly ILogger _logger;

        public ReviewController(
            IReviewViewModelService apartmentViewModelService,
            ILogger<ReviewController> logger)
        {
            _reviewViewModelService = apartmentViewModelService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(ReviewOptions reviewOptions)
        {
            var newReview = new ReviewViewModel();
            return View(newReview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _reviewViewModelService.CreateReviewAsync(viewModel);
                return RedirectToAction("Index","Apartment");
            }
            else
            {
                return View();
            }
        }
    }
}

using Booking.ApplicationCore.Enum;
using Booking.Web.Models;
using Booking.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces;

namespace Booking.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServiceViewModelService _accountService;

        public AccountController(IAccountServiceViewModelService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);

                if (response.StatusCode == ApplicationCore.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index", "Apartment");
                }

                ModelState.AddModelError("", response.Description);
            }

            return View(model);
        }
    }
}

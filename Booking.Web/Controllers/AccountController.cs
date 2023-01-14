using Booking.ApplicationCore.Enum;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);

                if (response.StatusCode == ApplicationCore.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", response.Description);
            }

            return View(model);
        }
    }
}

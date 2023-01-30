using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Booking.Web.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NuGet.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Azure;

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
                    Authenticate(response.Data);
					return RedirectToAction("Index", "City");
                }

                ModelState.AddModelError("", response.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);

                if(response.StatusCode == ApplicationCore.Enum.StatusCode.OK)
                {
                    Authenticate(response.Data);


					return RedirectToAction("Index", "City");
				}

                ModelState.AddModelError("", response?.Description);
			}

            return View(model);
        }

        private IActionResult Authenticate(string jwtToken)
        {
			HttpContext.Response.Cookies.Append("Booking.Application.Id", jwtToken,
            new CookieOptions
            {
	            MaxAge = TimeSpan.FromMinutes(60)
            });

            return Ok();
		}
    }
}

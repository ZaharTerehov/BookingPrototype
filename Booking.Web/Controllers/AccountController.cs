using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Booking.Web.Interfaces;
using Booking.Web.Services.Account;

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
                    SetAccessTokenAndRefreshToken(response.Data);
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
                    await SetAccessTokenAndRefreshToken(response.Data);

					return RedirectToAction("Index", "City");
				}

                ModelState.AddModelError("", response?.Description);
			}

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete(_accountService.LocationAccessToken);
            HttpContext.Response.Cookies.Delete(_accountService.LocationRefreshToken);

            return Redirect(Request.Headers["Referer"].ToString());
        }


        private async Task<IActionResult> SetAccessTokenAndRefreshToken(JwtTokenResult jwtToken)
        {
			HttpContext.Response.Cookies.Append(_accountService.LocationAccessToken, jwtToken.AccessToken,
            new CookieOptions
            {
                Expires = jwtToken.RefreshToken.Expires
            });

            HttpContext.Response.Cookies.Append(_accountService.LocationRefreshToken, jwtToken.RefreshToken.Token,
            new CookieOptions
            {
                Expires = jwtToken.RefreshToken.Expires
            });

            return Ok();
		}
    }
}

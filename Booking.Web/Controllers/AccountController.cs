using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Booking.Web.Interfaces;
using Booking.Web.Services.Account;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.PeopleService.v1;
using Newtonsoft.Json.Linq;

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
                ModelState.AddModelError("", response.Description!);
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
                    var result = _accountService.GetNameAndAvatar(model.Login);
                    SetNameAndAvatar(result.Result.name, result.Result.avatar);

                    await SetAccessTokenAndRefreshToken(response.Data!);
                    return Redirect(Request.Headers["Referer"].ToString());
                }

                ModelState.AddModelError("", response?.Description!);
			}

            return View(model);
        }

        private void SetNameAndAvatar(string name, string avatar)
        {
            HttpContext.Response.Cookies.Append("Name", name,
            new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10)
            });
            HttpContext.Response.Cookies.Append("Avatar", avatar,
            new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10)
            });
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

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var result = await _accountService.ConfirmEmail(email, token);

            var nameAndAvatar = _accountService.GetNameAndAvatar(email);
            SetNameAndAvatar(nameAndAvatar.Result.name, nameAndAvatar.Result.avatar);

            if (result.StatusCode == ApplicationCore.Enum.StatusCode.OK)
            {
                await SetAccessTokenAndRefreshToken(result.Data!);
                return Content("<html><h1><string>Your email has been verified</strong></h1></html>", "text/html");
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<JsonResult> CheckUserForConfirmedEmail()
        {
            var token = Request.Cookies[_accountService.LocationAccessToken];
            return Json(await _accountService.CheckValidUser(token!));
        }

        [GoogleScopedAuthorize(PeopleServiceService.ScopeConstants.UserinfoProfile)]
        public async Task<IActionResult> LoginWithGoogle([FromServices] IGoogleAuthProvider auth)
        {
            var result = await _accountService.LoginWithGoogle(auth);

            SetNameAndAvatar($"{result.user.Name} {result.user.Surname}", result.user.Avatar);

            await SetAccessTokenAndRefreshToken(result.jwt);

            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            HttpContext.Response.Cookies.Delete(".AspNetCore.CookiesC1");
            HttpContext.Response.Cookies.Delete(".AspNetCore.CookiesC2");

            return RedirectToAction("Index", "Apartment");
        }
    }
}

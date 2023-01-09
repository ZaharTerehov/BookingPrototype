using Booking.Web.Models;
using System.Security.Claims;

namespace Booking.Web.Interfaces
{
    public interface IAccountService
    {
        Task<ClaimsIdentity> Register(RegisterViewModel model);

        //Task<ClaimsIdentity> Login(LoginViewModel model);

        //Task<bool> ChangePassword(ChangePasswordViewModel model);
    }
}

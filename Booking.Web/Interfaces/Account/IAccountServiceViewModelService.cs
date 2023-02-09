using Booking.ApplicationCore.Response;
using Booking.Web.Models;
using Booking.Web.Services.Account;

namespace Booking.Web.Interfaces
{
    public interface IAccountServiceViewModelService
    {
        Task<BaseResponse<JwtTokenResult>> Register(RegisterViewModel model);

		Task<BaseResponse<JwtTokenResult>> Login(LoginViewModel model);

        Task<string> UpdateUserValidity(string novalidToken, string refreshToken);

        Task<bool> CheckValidUser(string accessToken);
    }
}

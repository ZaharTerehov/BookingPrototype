using Booking.ApplicationCore.Response;
using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IAccountServiceViewModelService
    {
        ValueTask<BaseResponse> Register(RegisterViewModel model);

		ValueTask<BaseResponse> Login(LoginViewModel model);
    }
}

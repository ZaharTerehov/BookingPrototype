using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.Response;
using Booking.Web.Models;
using System.Security.Claims;

namespace Booking.Web.Interfaces
{
    public interface IAccountServiceViewModelService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
    }
}

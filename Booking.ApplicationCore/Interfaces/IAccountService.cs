using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.Response;
using System.Security.Claims;

namespace Booking.ApplicationCore.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(User model);
    }
}

using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Interfaces
{
    public interface IAccountService
    {
        public Task<BaseResponse<ClaimsIdentity>> Register(User model);

        //Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        //Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
    }
}

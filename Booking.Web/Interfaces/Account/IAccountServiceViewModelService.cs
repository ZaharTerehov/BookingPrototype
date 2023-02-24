﻿using Booking.ApplicationCore.Response;
using Booking.Web.Models;
using Booking.Web.Services.Account;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Interfaces
{
    public interface IAccountServiceViewModelService
    {
        public string LocationAccessToken { get; init; }
        public string LocationRefreshToken { get; init; }

        Task<BaseResponse<JwtTokenResult>> Register(RegisterViewModel model);

		Task<BaseResponse<JwtTokenResult>> Login(LoginViewModel model);

        Task<JwtTokenResult> UpdateUserValidity(string novalidToken, string refreshToken);

        Task<bool> CheckValidUser(string accessToken);

        Task<BaseResponse<JwtTokenResult>> ConfirmEmail(int userId, string token);

        Task<JwtTokenResult> LoginWithGoogle([FromServices] IGoogleAuthProvider auth);
    }
}

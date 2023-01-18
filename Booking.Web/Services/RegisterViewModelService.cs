using AutoMapper;
using Booking.ApplicationCore.Enum;
using Booking.ApplicationCore.Helpers;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.ApplicationCore.Response;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.ContentModel;
using System.Security.Claims;

namespace Booking.Web.Services
{
    public sealed class RegisterViewModelService : IAccountServiceViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterViewModelService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var options = new QueryOptions<User>().SetFilterOption(y => y.Email == model.Email);
                var users = await _unitOfWork.Users.GetAllAsync(options);

                if (users.Count > 0)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "There is already a user with this login",
                    };
                }

                var newUser = new User()
                {
                    Role = Role.User,
                    Name = model.Name,
                    Email = model.Email,
                    Surname = model.Surname,
                    DateOfBirth = model.DateOfBirth,
                    NumberPhone = model.NumberPhone,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                };

                await _unitOfWork.Users.CreateAsync(newUser);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = new ClaimsIdentity(),
                    Description = "Object addded",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}

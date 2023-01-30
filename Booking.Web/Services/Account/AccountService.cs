using AutoMapper;
using Booking.ApplicationCore.Enum;
using Booking.ApplicationCore.Helpers;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.ApplicationCore.Response;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Interfaces.Login;

namespace Booking.Web.Services
{
    public sealed class AccountService : IAccountServiceViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IJwtProvider _jwtProvider;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async ValueTask<BaseResponse> Register(RegisterViewModel model)
        {
            try
            {
                var optionsEmail = new QueryEntityOptions<User>().SetFilterOption(y => y.Email == model.Email);
                var users = await _unitOfWork.Users.GetAllAsync(optionsEmail);

                if (users.Count > 0)
                {
                    return new BaseResponse()
                    {
                        Description = "There is already a user with this login",
                    };
                }

                var optionsId = new QueryEntityOptions<User>().AddSortOption(true, y => y.Id);
                var entities = await _unitOfWork.Users.GetAllAsync(optionsId);

                var id = 1;

                if (entities.Count > 0)
                {
                    id = entities[0].Id;
                    id++;
                }

                var newUser = new User()
                {
					Id = id,
					Role = Role.User,
                    Name = model.Name,
                    Email = model.Email,
                    Surname = model.Surname,
                    DateOfBirth = model.DateOfBirth,
                    NumberPhone = model.NumberPhone,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                };

                await _unitOfWork.Users.CreateAsync(newUser);
                var result = Authenticate(newUser);

                return new BaseResponse()
                {
                    Data = result,
                    Description = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async ValueTask<BaseResponse> Login(LoginViewModel model)
        {
            try
            {
                var options = new QueryEntityOptions<User>().SetFilterOption(y => y.Email == model.Login);
                var user = await _unitOfWork.Users.GetAllAsync(options);

                if (user is null)
                {
                    return new BaseResponse("User is not found", StatusCode.InternalServerError, "");
                }

                if (user[0].Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse()
                    {
                        Description = "Invalid password or login"
                    };
                }

                var result = Authenticate(user[0]);

                return new BaseResponse()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private string Authenticate(User user)
        {
            var tokenResult = _jwtProvider.Generate(user);

            return tokenResult.AccessToken;
        }
    }
}

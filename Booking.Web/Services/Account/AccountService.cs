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
using Booking.Web.Services.Account;

namespace Booking.Web.Services
{
    public sealed class AccountService : IAccountServiceViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly ITokenService _jwtProvider;

        public string LocationAccessToken { get; init; } = "Booking.Application.Id";
        public string LocationRefreshToken { get; init; } = "Booking.Application.IdR";

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, ITokenService jwtProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<BaseResponse<JwtTokenResult>> Register(RegisterViewModel model)
        {
            try
            {
                var optionsEmail = new QueryEntityOptions<User>().SetFilterOption(y => y.Email == model.Email);
                var users = await _unitOfWork.Users.GetAllAsync(optionsEmail);

                if (users.Count > 0)
                {
                    return new BaseResponse<JwtTokenResult>()
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
                
                var result = await Authenticate(newUser);

                newUser.RefreshToken = result.RefreshToken.Token;
                newUser.RefreshTokenExpiryInMinutes = result.RefreshToken.Expires;

                await _unitOfWork.Users.CreateAsync(newUser);

                return new BaseResponse<JwtTokenResult>()
                {
                    Data = result,
                    Description = result.AccessToken,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<JwtTokenResult>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<JwtTokenResult>> Login(LoginViewModel model)
        {
            try
            {
                var options = new QueryEntityOptions<User>().SetFilterOption(y => y.Email == model.Login);
                var users = await _unitOfWork.Users.GetAllAsync(options);

                if (users is null)
                {
                    return new BaseResponse<JwtTokenResult>()
                    {
                        Description = "User is not found",
                        StatusCode = StatusCode.InternalServerError
                    };
                }

                var user = users.First();

                if (user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<JwtTokenResult>()
                    {
                        Description = "Invalid password or login"
                    };
                }

                var result = await Authenticate(user);

                var existingUser = await _unitOfWork.Users.GetByIdAsync(user.Id);

                existingUser.UpdateRefreshToken(result.RefreshToken.Token, result.RefreshToken.Expires);

                await _unitOfWork.Users.UpdateAsync(existingUser);

                return new BaseResponse<JwtTokenResult>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<JwtTokenResult>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private async Task<JwtTokenResult> Authenticate(User user)
        {
            var tokenResult = await _jwtProvider.GenerateAccessToken(user);

            var refreshToken = await _jwtProvider.GenerateRefreshToken();

            return new JwtTokenResult()
            {
                AccessToken = tokenResult.AccessToken,
                AccessTokenExpires = tokenResult.AccessTokenExpires,
                RefreshToken = refreshToken,
            };
        }

        public Task<bool> CheckValidUser(string accessToken)
        {
            return _jwtProvider.ValidateAccessToken(accessToken);
        }

        public async Task<string> UpdateUserValidity(string novalidToken, string refreshToken)
        {
            var principal = await _jwtProvider.GetPrincipalFromExpiredToken(novalidToken);

            string email = principal.Claims.First(claim => claim.Type == "sub").Value;

            var options = new QueryEntityOptions<User>().SetFilterOption(y => y.Email == email);
            var users = await _unitOfWork.Users.GetAllAsync(options);
            var user = users.First();

            if (user.RefreshToken == refreshToken && user.RefreshTokenExpiryInMinutes > DateTime.Now)
            {
                var tokenResult = await _jwtProvider.GenerateAccessToken(user);

                var existingUser = await _unitOfWork.Users.GetByIdAsync(user.Id);

                return tokenResult.AccessToken;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

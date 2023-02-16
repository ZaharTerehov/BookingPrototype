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
using Booking.Web.Interfaces.Account;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Booking.Web.Services
{
    public sealed class AccountService : IAccountServiceViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly ITokenService _jwtProvider;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IEmailSender _emailSender;

        public string LocationAccessToken { get; init; } = "Booking.Application.Id";
        public string LocationRefreshToken { get; init; } = "Booking.Application.IdR";

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, ITokenService jwtProvider, 
            ICaptchaValidator captchaValidator, IEmailSender emailSender)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _captchaValidator = captchaValidator;
            _emailSender = emailSender;
        }

        public async Task<BaseResponse<JwtTokenResult>> Register(RegisterViewModel model)
        {
            try
            {
                var captchaValidation = await СheckСaptchaTokenForValidity(model.ReCaptcha);

                if (captchaValidation.StatusCode != StatusCode.OK)
                    return captchaValidation;

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
                    Password = HashPasswordHelper.HashPassword(model.Password)
                };

                await _unitOfWork.Users.CreateAsync(newUser);

                var token = await GenerateEmailConfirmationToken(newUser);

                newUser.EmailVerificationToken = token.token;

                await _unitOfWork.Users.UpdateAsync(newUser);

                await _emailSender.SendEmailAsync(newUser.Email, "Confirm your email", "Click on the link to confirm email - " + token.link);


                return new BaseResponse<JwtTokenResult>()
                {
                    Description = "Confirm your email",
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

        private async Task<(string link, string token)> GenerateEmailConfirmationToken(User user)
        {
            var confirmEmailToken = await _jwtProvider.GenerateConfirmEmailToken(user);

            var validEmailToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmEmailToken));

            return ($"https://localhost:7048/Account/ConfirmEmail?userid={user.Id}&token={validEmailToken}", validEmailToken);
        }

        public async Task<BaseResponse<JwtTokenResult>> ConfirmEmail(int userId, string token)
        {
            var existingUser = await _unitOfWork.Users.GetByIdAsync(userId);

            if(existingUser != null && existingUser.EmailVerificationToken == token)
            {
                existingUser.EmailIsVerified = true;

                var result = await Authenticate(existingUser);

                existingUser.UpdateRefreshToken(result.RefreshToken.Token, result.RefreshToken.Expires);

                await _unitOfWork.Users.UpdateAsync(existingUser);

                return new BaseResponse<JwtTokenResult>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<JwtTokenResult>()
            {
                Description = "User not found",
                StatusCode = StatusCode.UserNotFound
            };
        }

        private async Task<BaseResponse<JwtTokenResult>> СheckСaptchaTokenForValidity(string token)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(token))
            {
                return new BaseResponse<JwtTokenResult>()
                {
                    Description = "Captcha validation failed",
                    StatusCode = StatusCode.InternalServerError
                };
            }

            return new BaseResponse<JwtTokenResult>()
            {
                Description = "Captcha is valid",
                StatusCode = StatusCode.OK
            };
        }

        public async Task<BaseResponse<JwtTokenResult>> Login(LoginViewModel model)
        {
            try
            {
                var captchaValidation = await СheckСaptchaTokenForValidity(model.ReCaptcha);

                if (captchaValidation.StatusCode != StatusCode.OK)
                    return captchaValidation;

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

                if(!user.EmailIsVerified)
                {
                    var token = await GenerateEmailConfirmationToken(user);

                    user.EmailVerificationToken = token.token;

                    await _unitOfWork.Users.UpdateAsync(user);

                    await _emailSender.SendEmailAsync(user.Email, "Confirm your email", "Click on the link to confirm email - " + token.link);

                    return new BaseResponse<JwtTokenResult>()
                    {
                        Description = "Confirm your email",
                        StatusCode = StatusCode.OK
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

        public async Task<JwtTokenResult> UpdateUserValidity(string novalidToken, string refreshToken)
        {
            var principal = await _jwtProvider.GetPrincipalFromExpiredToken(novalidToken);

            string email = principal.Claims.First(claim => claim.Type == "sub").Value;

            var options = new QueryEntityOptions<User>().SetFilterOption(y => y.Email == email);
            var users = await _unitOfWork.Users.GetAllAsync(options); 

            if (users.Count == 0)
                return null;
            var user = users.First();

            if (user.RefreshToken == refreshToken 
                && user.RefreshTokenExpiryInMinutes > DateTime.Now)
            {
                var tokenResult = await _jwtProvider.GenerateAccessToken(user);
                var existingUser = await _unitOfWork.Users.GetByIdAsync(user.Id);
                return tokenResult;
            }
            else
                return null;
        }
    }
}

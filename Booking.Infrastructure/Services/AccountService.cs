using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<ApartmentTypeRepository> _logger;

        public AccountService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<BaseResponse<ClaimsIdentity>> Register(User model)
        {
            throw new NotImplementedException();
        }
    }
}

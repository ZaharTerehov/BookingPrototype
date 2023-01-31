﻿using Booking.ApplicationCore.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Web.Interfaces;
using Booking.Web.Interfaces.Login;
using Booking.Web.Services;
using Booking.Web.Services.Account;
using Microsoft.AspNetCore.Identity;

namespace Booking.Web.Configuration
{
    public static class ConfigureCoreService
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddScoped(typeof(ICityViewModelService), typeof(CityViewModelService));
            services.AddScoped(typeof(IApartmentViewModelService),typeof(ApartmentViewModelService));
            services.AddScoped(typeof(IApartmentTypeViewModelService),typeof(ApartmentTypeViewModelService));
            services.AddScoped(typeof(ICountryViewModelService), typeof(CountryViewModelService));
            services.AddScoped(typeof(IAccountServiceViewModelService), typeof(AccountService));
            services.AddScoped(typeof(IReservationViewModerService), typeof(ReservationViewModelService));
            return services;
        }

    }
}

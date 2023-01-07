using Booking.ApplicationCore.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Web.Interfaces;
using Booking.Web.Services;

namespace Booking.Web.Configuration
{
    public static class ConfigureCoreService
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IApartmentTypeViewModelService),typeof(ApartmentTypeViewModelService));
            services.AddScoped(typeof(ICountryViewModelService), typeof(CountryViewModelService));
            return services;
        }

    }
}

using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Infrastructure.Services;
using Booking.Web.Interfaces;
using Booking.Web.Services;

namespace Booking.Web.Configuration
{
    public static class ConfigureCoreService
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<ApartmentType>), typeof(ApartmentTypeRepository));
            services.AddScoped(typeof(IApartmentTypeViewModelService),typeof(ApartmentTypeViewModelService));
            return services;
        }

    }
}

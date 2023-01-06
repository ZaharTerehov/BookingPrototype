using Booking.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data
{
    public class BookingContextSeed
    {
        public static async Task SeedAsync(BookingContext bookingContext, ILogger logger, int retry =0)
        {
            var retryForAvailability = retry;

            if (!await bookingContext.ApartmentTypes.AnyAsync())
            {
                logger.LogInformation("Table ApartmentTypes is empty");

                await bookingContext.ApartmentTypes.AddRangeAsync(GetInitialApartmentTypes());

                await bookingContext.SaveChangesAsync();

                logger.LogInformation("Seed database ApartmentTypes complete");
            }
        }

        private static IEnumerable<ApartmentType> GetInitialApartmentTypes()
        {
            return new List<ApartmentType>
            {
                new ApartmentType { Name = "House" },
                new ApartmentType { Name = "HotelRoom" },
                new ApartmentType { Name = "Apartment" },
            };
        }
    }
}

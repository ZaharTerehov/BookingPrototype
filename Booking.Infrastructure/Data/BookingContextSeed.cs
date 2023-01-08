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
            
            if (!await bookingContext.Countries.AnyAsync())
            {
                logger.LogInformation("Table Countries is empty");

                await bookingContext.Countries.AddRangeAsync(GetInitialCountries());

                await bookingContext.SaveChangesAsync();

                logger.LogInformation("Seed database Countries complete");
            }
            
            if (!await bookingContext.Cities.AnyAsync())
            {
                logger.LogInformation("Table Cities is empty");

                await bookingContext.Cities.AddRangeAsync(GetInitialCities());

                await bookingContext.SaveChangesAsync();

                logger.LogInformation("Seed database Cities complete");
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

        private static IEnumerable<Country> GetInitialCountries()
        {
            return new List<Country>
            {
                new Country { Name = "Belarus" },
                new Country { Name = "Poland" },
                new Country { Name = "Italy" },
                new Country { Name = "France"},
                new Country { Name = "Spain"},
            };
        }

        private static IEnumerable<City> GetInitialCities()
        {
            return new List<City>
            {
                new City { Name = "Brest", CountryId = 1 },
                new City { Name =  "Paris", CountryId = 4},
                new City { Name = "Katowice", CountryId = 2 }
            };
        }
    }
}

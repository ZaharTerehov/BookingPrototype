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

            if (!await bookingContext.Apartments.AnyAsync())
            {
                logger.LogInformation("Table Apartment is empty");

                await bookingContext.Apartments.AddRangeAsync(GetInitialApartments());

                await bookingContext.SaveChangesAsync();

                logger.LogInformation("Seed database Apartments complete");
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

        private static IEnumerable<Apartment> GetInitialApartments()
        {
            return new List<Apartment>
            {
                new Apartment
                {
                    Name = "Sealoft 4 Townhouse",
                    Description = "Set in Myrtle Beach in the South Carolina region, Sealoft 4 Townhouse features a balcony. " +
                    "The air-conditioned accommodation is 300 m from Surfside Beach, " +
                    "and guests benefit from complimentary WiFi and private parking available on site.",
                    Price = 1230m,
                    Picture = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/306235173.jpg?k=ca9831e00d69fa3ca98c5888a0071c2772f2be3bd822a0529c45ffbd7e40ffa4&o=&hp=1"
                },
                new Apartment
                {
                    Name = "Villas at Marina Inn at Grande Dunes",
                    Description = "Offering and indoor and outdoor pool, these self-catering Myrtle Beach villas have a " +
                    "full kitchen and a balcony with a view. Free WiFi and a free beach transfer service are provided.\r\n\r\n" +
                    "Fully furnished, these villas feature a separate seating area with a cable TV and a sofa. " +
                    "Extras at Villas at Marina Inn at Grande Dunes include a hairdryer, towels, and linen.",
                    Price = 1450m,
                    Picture = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/195864924.jpg?k=eb9cc2ad6342214068041b01c05c24730536c9948cf769c8f706e594b8d412f5&o=&hp=1"
                }
            };
        }
    }
}

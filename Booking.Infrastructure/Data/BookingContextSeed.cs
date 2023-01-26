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

            //if (!await bookingContext.Users.AnyAsync())
            //{
            //    logger.LogInformation("Table User is empty");

            //    await bookingContext.Apartments.AddRangeAsync(GetInitialApartments());

            //    await bookingContext.SaveChangesAsync();

            //    logger.LogInformation("Seed database User complete");
            //}
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
                    ApartmentTypeId = 1,
                    Name = "Sealoft 4 Townhouse",
                    Description = "Set in Myrtle Beach in the South Carolina region, Sealoft 4 Townhouse features a balcony. " +
                    "The air-conditioned accommodation is 300 m from Surfside Beach, " +
                    "and guests benefit from complimentary WiFi and private parking available on site.",
                    Price = 1230m,
                    CityId = 1,
                    Picture = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/306235173.jpg?k=ca9831e00d69fa3ca98c5888a0071c2772f2be3bd822a0529c45ffbd7e40ffa4&o=&hp=1"
                },
                new Apartment
                {
                    ApartmentTypeId = 1,
                    Name = "Sealoft 4 Townhouse",
                    Description = "Set in Myrtle Beach in the South Carolina region, Sealoft 4 Townhouse features a balcony. " +
                    "The air-conditioned accommodation is 300 m from Surfside Beach, " +
                    "and guests benefit from complimentary WiFi and private parking available on site.",
                    Price = 1200m,
                    CityId = 1,
                    Picture = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/400539840.jpg?k=84ac662766f1bcb34f42669e477ff3e2a2fe49420cede123ea2fa2ccfb011d7d&o=&hp=1"
                },
                new Apartment
                {
                    ApartmentTypeId = 1,
                    Name = "Sealoft 4 Townhouse",
                    Description = "Set in Myrtle Beach in the South Carolina region, Sealoft 4 Townhouse features a balcony. " +
                    "The air-conditioned accommodation is 300 m from Surfside Beach, " +
                    "and guests benefit from complimentary WiFi and private parking available on site.",
                    Price = 1150m,
                    CityId = 2,
                    Picture = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/400539852.jpg?k=9446171beb45d9bd52078e36cad51f33e90401c8b62fcd05ec9facf82f17ad57&o=&hp=1"
                },
                new Apartment
                {
                    ApartmentTypeId = 2,
                    Name = "Villas at Marina Inn at Grande Dunes",
                    Description = "Offering and indoor and outdoor pool, these self-catering Myrtle Beach villas have a " +
                    "full kitchen and a balcony with a view. Free WiFi and a free beach transfer service are provided.\r\n\r\n" +
                    "Fully furnished, these villas feature a separate seating area with a cable TV and a sofa. " +
                    "Extras at Villas at Marina Inn at Grande Dunes include a hairdryer, towels, and linen.",
                    Price = 1450m,
                    CityId = 3,
                    Picture = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/195864924.jpg?k=eb9cc2ad6342214068041b01c05c24730536c9948cf769c8f706e594b8d412f5&o=&hp=1"
                },
                new Apartment
                {
                    ApartmentTypeId = 3,
                    Name = "Villa Pastor",
                    Description = "Set in Cala Anguila, 150 meters from Cala Anguila Beach and 2.5 km from Playa de Porto Cristo, Villa Pastor offers " + 
                    "air conditioning. This villa with a private pool features a garden and free private parking. Barbecue facilities are provided. " + 
                    "Free Wi-Fi is provided.\r\n\r\nThe villa has 3 bedrooms, 2 bathrooms, linens, towels, satellite TV, a dining area, a fully " + 
                    "equipped kitchen and a terrace overlooking the river.\r\n\r\nGuests can take a dip in the outdoor pool.",
                    Price = 1100m,
                    CityId = 3,
                    Picture = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/345550502.jpg?k=e2068b7712ffc925f403cff7572fc0dbbd0cd1d006916b936de539f8be774128&o=&hp=1"
                }
            };
        }
    }
}

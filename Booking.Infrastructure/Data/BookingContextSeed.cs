using Booking.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var a1 = new Apartment
            {
                ApartmentTypeId = 1,
                Name = "Sealoft 4 Townhouse",
                Description = "Set in Myrtle Beach in the South Carolina region, Sealoft 4 Townhouse features a balcony. " +
                   "The air-conditioned accommodation is 300 m from Surfside Beach, " +
                   "and guests benefit from complimentary WiFi and private parking available on site.",
                Price = 1230m,
                CityId = 1,
                Address = "Minskaya, 56",
                PeopleNumber = 4,
            };
            var a2 = new Apartment
            {
                ApartmentTypeId = 1,
                Name = "Sealoft 4 Townhouse",
                Description = "Set in Myrtle Beach in the South Carolina region, Sealoft 4 Townhouse features a balcony. " +
                    "The air-conditioned accommodation is 300 m from Surfside Beach, " +
                    "and guests benefit from complimentary WiFi and private parking available on site.",
                Price = 1200m,
                CityId = 1,                
                Address = "Mitskevicha, 25",
                PeopleNumber = 2,
            };
            var a3 = new Apartment
            {
                ApartmentTypeId = 1,
                Name = "Sealoft 4 Townhouse",
                Description = "Set in Myrtle Beach in the South Carolina region, Sealoft 4 Townhouse features a balcony. " +
                    "The air-conditioned accommodation is 300 m from Surfside Beach, " +
                    "and guests benefit from complimentary WiFi and private parking available on site.",
                Price = 1150m,
                CityId = 2,
                Address = "Pier Rishar, 4",
                PeopleNumber = 2,
            };
            var a4 = new Apartment
            {
                ApartmentTypeId = 2,
                Name = "Villas at Marina Inn at Grande Dunes",
                Description = "Offering and indoor and outdoor pool, these self-catering Myrtle Beach villas have a " +
                    "full kitchen and a balcony with a view. Free WiFi and a free beach transfer service are provided.\r\n\r\n" +
                    "Fully furnished, these villas feature a separate seating area with a cable TV and a sofa. " +
                    "Extras at Villas at Marina Inn at Grande Dunes include a hairdryer, towels, and linen.",
                Price = 1450m,
                CityId = 3,
                Address = "Pilsudskego, 114",
                PeopleNumber = 4,
            };
            var a5 = new Apartment
            {
                ApartmentTypeId = 3,
                Name = "Villa Pastor",
                Description = "Set in Cala Anguila, 150 meters from Cala Anguila Beach and 2.5 km from Playa de Porto Cristo, Villa Pastor offers " +
                    "air conditioning. This villa with a private pool features a garden and free private parking. Barbecue facilities are provided. " +
                    "Free Wi-Fi is provided.\r\n\r\nThe villa has 3 bedrooms, 2 bathrooms, linens, towels, satellite TV, a dining area, a fully " +
                    "equipped kitchen and a terrace overlooking the river.\r\n\r\nGuests can take a dip in the outdoor pool.",
                Price = 1100m,
                CityId = 3,
                Address = "Sklodowska-Kuri, 56",
                PeopleNumber = 3,
            };

            a1.Pictures.Add(new ApartmentPicture { PictureUrl = "Images\\1.jpg" });
            a2.Pictures.Add(new ApartmentPicture { PictureUrl = "Images\\2.jpg" });
            a3.Pictures.Add(new ApartmentPicture { PictureUrl = "Images\\3.jpg" });
            a4.Pictures.Add(new ApartmentPicture { PictureUrl = "Images\\4.jpg" });
            a5.Pictures.Add(new ApartmentPicture { PictureUrl = "Images\\5.jpg" });
            
            return new List<Apartment>{a1, a2, a3, a4, a5};
        }
    }
}

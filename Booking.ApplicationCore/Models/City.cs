using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models
{
    public class City
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public Country? Country { get; set; } 

        public int? CountryId { get; set; }

        public void UpdateDetails(CityDetails details)
        {
            Name = details.Name;
            CountryId = details.CountryId;
        }

        public readonly record struct CityDetails
        {
            public string? Name { get; }
            public int? CountryId { get; }

            public CityDetails(string? name, int? countryId)
            {
                Name = name;
                CountryId = countryId;
            }
        }

    }
}

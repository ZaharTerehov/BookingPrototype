using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models
{
    public class City : BaseModel
    {
        [Required]
        [StringLength(100)]
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

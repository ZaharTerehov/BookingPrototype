using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models
{
    public sealed class Country : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } 

        public void UpdateDetails(CountryDetails details)
        {
            Name = details.Name;
        }

        public readonly record struct CountryDetails
        {
            public string? Name { get; }

            public CountryDetails(string? name)
            {
                Name = name;
            }
        }
    }
}

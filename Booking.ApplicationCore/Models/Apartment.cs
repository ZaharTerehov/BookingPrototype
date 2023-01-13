using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models
{
    public sealed class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public void UpdateDetails(ApartmentDetails details)
        {
            Name = details.Name;
            Description = details.Description;
            Price = details.Price;
            Picture = details.Picture;
        }

        public readonly record struct ApartmentDetails
        {
            public string? Name { get; }
            public string? Description { get; }

            public decimal Price { get; }

            public string? Picture { get;  }

            public ApartmentDetails(string? name, string? description, decimal price, string? picture)
            {
                Name = name;
                Description = description;
                Price = price;
                Picture = picture;
            }
        }
    }
}

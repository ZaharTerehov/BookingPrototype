using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Models
{
    public sealed class Apartment : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }
        public City? City { get; set; }
        public int? CityId { get; set; }

        public int? ApartmentTypeId { get; set; }
        public ApartmentType? ApartmentType { get; set; }
    

        public void UpdateDetails(ApartmentDetails details)
        {
            Name = details.Name;
            Description = details.Description;
            Price = details.Price;
            Picture = details.Picture;
            CityId = details.CityId;
            ApartmentTypeId = details.ApartmentTypeId;
        }

        public readonly record struct ApartmentDetails
        {
            public string? Name { get; }
            public string? Description { get; }

            public decimal Price { get; }

            public string? Picture { get;  }
            public int? CityId { get; }
            public int? ApartmentTypeId { get;  }

            public ApartmentDetails(string? name, string? description, decimal price, string? picture, int? apartmentTypeId)
            {
                Name = name;
                Description = description;
                Price = price;
                Picture = picture;
                ApartmentTypeId = apartmentTypeId;

                //TODO add option to change for City 
            }
        }
    }
}

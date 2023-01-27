using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Address { get; set; }
        [Range(1, 10)]
        public byte PeopleNumber { get; set; }

        public City? City { get; set; }
        public int? CityId { get; set; }
        
        public ApartmentType? ApartmentType { get; set; }
        public int? ApartmentTypeId { get; set; }

        public void UpdateDetails(ApartmentDetails details)
        {
            Name = details.Name;
            Description = details.Description;
            Price = details.Price;
            Picture = details.Picture;
            CityId = details.CityId;
            ApartmentTypeId = details.ApartmentTypeId;
            Address = details.Address;
            PeopleNumber = details.PeopleNumber;
        }

        public readonly record struct ApartmentDetails
        {
            public string? Name { get; }
            public string? Description { get; }

            public decimal Price { get; }

            public string? Picture { get;  }
            public int? CityId { get; }
            public int? ApartmentTypeId { get;  }
            public string? Address { get; }
            public byte PeopleNumber { get; }

            public ApartmentDetails(string? name, string? description, decimal price, string? picture, int? apartmentTypeId, int? cityId,
                                    string? address, byte peopleNumber)
            {
                Name = name;
                Description = description;
                Price = price;
                Picture = picture;
                ApartmentTypeId = apartmentTypeId;
                CityId= cityId;
                Address = address;
                PeopleNumber = peopleNumber;
            }
        }
    }
}

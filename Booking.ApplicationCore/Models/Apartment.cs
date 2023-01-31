using Booking.ApplicationCore.Constants;
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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(ApplicationConstants.MinPeopleNumber, ApplicationConstants.MaxPeopleNumber)]
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

            public string? Picture { get; }
            public int? CityId { get; }
            public int? ApartmentTypeId { get; }
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
                CityId = cityId;
                Address = address;
                PeopleNumber = peopleNumber;
            }
        }
    }
}

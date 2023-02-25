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
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public IList<ApartmentPicture> Pictures { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        [Range(ApplicationConstants.MinPeopleNumber, ApplicationConstants.MaxPeopleNumber)]
        public byte PeopleNumber { get; set; }

        public City? City { get; set; }
        public int? CityId { get; set; }

        public ApartmentType? ApartmentType { get; set; }
        public int? ApartmentTypeId { get; set; }

        public IList<Review>? Reviews { get; set; }

        public IList<Reservation>? Reservations { get; set; }
        public Apartment()
        {
            Pictures = new List<ApartmentPicture>();
        }
        public void UpdateDetails(ApartmentDetails details)
        {
            Name = details.Name;
            Description = details.Description;
            Price = details.Price;
            Pictures = details.Pictures?.Count >0 ? details.Pictures: Pictures;
            CityId = details.CityId;
            ApartmentTypeId = details.ApartmentTypeId;
            Address = details.Address;
            PeopleNumber = details.PeopleNumber;
            Reservations = new List<Reservation>();
        }

        public readonly record struct ApartmentDetails
        {
            public string? Name { get; }
            public string? Description { get; }

            public decimal Price { get; }

            public IList<ApartmentPicture>? Pictures { get; }
            public int? CityId { get; }
            public int? ApartmentTypeId { get; }
            public string? Address { get; }
            public byte PeopleNumber { get; }

            public ApartmentDetails(string? name, string? description, decimal price, IList<ApartmentPicture>? pictures, int? apartmentTypeId, int? cityId,
                                    string? address, byte peopleNumber)
            {
                Name = name;
                Description = description;
                Price = price;                
                Pictures = pictures;
                ApartmentTypeId = apartmentTypeId;
                CityId = cityId;
                Address = address;
                PeopleNumber = peopleNumber;
            }
        }
    }
}

using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Services
{
    public sealed class ApartmentTypeRepository : IRepository<ApartmentType>
    {
        private static IList<ApartmentType> _apartmentTypes = new List<ApartmentType>
        {
            new ApartmentType { Id = 1, Name = "House" },
            new ApartmentType { Id = 2, Name = "HotelRoom" },
            new ApartmentType { Id = 3, Name = "Apartment" }
        };

        public void Create(ApartmentType entity)
        {
            if (_apartmentTypes.Any(x => x.Name == entity.Name))
            {
                throw new Exception("This apartment type is already created");
            }
            _apartmentTypes.Add(entity);
        }

        public IEnumerable<ApartmentType> GetAll()
        {
            return _apartmentTypes;
        }

        public ApartmentType? GetById(int id)
        {
            var foundApartmentType = _apartmentTypes.FirstOrDefault(x => x.Id==id);
            return foundApartmentType;
        }

        public void Update(ApartmentType entity)
        {
            var existedApartmentType = GetById(entity.Id);
            if (existedApartmentType!=null)
            {
                int index = _apartmentTypes.IndexOf(existedApartmentType);
                _apartmentTypes[index] = entity;
            }
        }
        
    }
}

using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Services
{
    public sealed class ApartmentTypeRepository : IRepository<ApartmentType>
    {
        private readonly ILogger<ApartmentTypeRepository> _logger;
        

        public ApartmentTypeRepository(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ApartmentTypeRepository>();
        }

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

            _logger.LogInformation($"Create new instance of {entity.GetType().Name} : {entity.Name}");
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

        public void Delete(int id)
        {

            var existedApartmentType = GetById(id);
            if (existedApartmentType!=null)
            {
                int index = _apartmentTypes.IndexOf(existedApartmentType);
                _apartmentTypes.RemoveAt(index);
            }
        }

        public Task<List<ApartmentType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApartmentType?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApartmentType entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(ApartmentType entity)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(ApartmentType entity)
        {
            throw new NotImplementedException();
        }
    }
}

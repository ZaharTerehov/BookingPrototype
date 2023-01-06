using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly BookingContext _dbBookingContext;

        public EFRepository(BookingContext bookingContext)
        {
            _dbBookingContext= bookingContext;
        }

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entities = await _dbBookingContext.Set<T>().ToListAsync();

            return entities; 
        }

        public T? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

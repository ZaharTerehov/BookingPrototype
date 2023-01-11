using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<T?> GetByIdAsync(int id)
        {
            var entities = await _dbBookingContext.Set<T>().FindAsync(id);
            return entities;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            var entities = await _dbBookingContext.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _dbBookingContext.Set<T>().Where(predicate).ToListAsync();
            return entities;
        }

        public async Task CreateAsync(T entity)
        {
            _dbBookingContext.Add(entity);
            await _dbBookingContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbBookingContext.Remove(entity);
            await _dbBookingContext.SaveChangesAsync();
        }            

        public async Task UpdateAsync(T entity)
        {
            _dbBookingContext.Update(entity);
            await _dbBookingContext.SaveChangesAsync();
        }
    }
}

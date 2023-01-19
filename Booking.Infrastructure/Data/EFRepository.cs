using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Infrastructure.Data.DBExtentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data
{
    public class EFRepository<T> : IRepository<T> where T : BaseModel
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

        public async Task<IList<T>> GetAllAsync(QueryEntityOptions<T> options)
        {
            return await _dbBookingContext.Set<T>().IncludeFields(options.IncludeOptions)
                                            .FilterEntities(options.FilterOption)
                                            .OrderEntityBy(options.SortOptions)
                                            .SkipTakeEntities(options.CurrentPage, options.PageSize)
                                            .ToListAsync();            
        }

        public async Task<IList<TVM>> GetAllViewModelAsync<TVM>(QueryViewModelOption<T, TVM> options) where TVM : class
        {
            return await _dbBookingContext.Set<T>().FilterEntities(options.FilterOption)
                                            .OrderEntityBy(options.SortOptions)
                                            .SelectEntities(options.SelectOption)
                                            .SkipTakeEntities(options.CurrentPage, options.PageSize)
                                            .ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            entity.DateCreated = DateTime.UtcNow;
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
            entity.DateUpdated = DateTime.UtcNow;
            _dbBookingContext.Update(entity);
            await _dbBookingContext.SaveChangesAsync();
        }
    }
}

using Booking.ApplicationCore.Extentions;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Infrastructure.Data.DBExtentions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Booking.Infrastructure.Data
{
    public class EFRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly BookingContext _dbBookingContext;

        public EFRepository(BookingContext bookingContext)
        {
            _dbBookingContext= bookingContext;
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var entities = await _dbBookingContext.Set<T>().IncludeFields(includes).FirstOrDefaultAsync(x => x.Id == id);
            return entities;
        }

        public async Task<IList<T>> GetAllAsync(QueryEntityOptions<T> options)
        {
            return await _dbBookingContext.Set<T>()
                                            .FromSqlQquery(options.SqlQuery)
                                            .IncludeFields(options.IncludeOptions)
                                            .FilterEntities(options.FilterOption)
                                            .OrderEntityBy(options.SortOptions)
                                            .SkipTakeEntities(options.PageOptions.CurrentPage, options.PageOptions.PageSize)
                                            .ToListAsync();            
        }

        public async Task<IList<Dto>> GetAllDtoAsync<Dto>(QueryViewModelOption<T, Dto> options)
        {
            return await _dbBookingContext.Set<T>()
                                            .FilterEntities(options.FilterOption)
                                            .OrderEntityBy(options.SortOptions)
                                            .SelectEntities(options.SelectOption)
                                            .SkipTakeEntities(options.PageOptions.CurrentPage, options.PageOptions.PageSize)
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

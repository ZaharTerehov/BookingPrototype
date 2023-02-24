using Booking.ApplicationCore.QueryOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : class 
    {
       // Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetAllAsync(QueryEntityOptions<T> options);
        Task<IList<TVM>> GetAllDtoAsync<TVM>(QueryViewModelOption<T, TVM> options); //where TVM : class; 
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity); 
    }
}

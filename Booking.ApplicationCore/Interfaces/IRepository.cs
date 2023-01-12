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
        Task<IList<T>> GetAllAsync(Expression < Func<T, bool>> selectCondition = null,
                                            params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity); 
    }
}

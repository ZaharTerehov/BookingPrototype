using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T? GetById(int id);
        void CreateAsync(T entity);
        void Update(T entity);
        void Delete(int id); 
        void DeleteAsync(T entity); 

        //void Save();
    }
}

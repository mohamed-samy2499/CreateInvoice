using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int? id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T item);
        Task<int> Update(T item);
        Task<int> Delete(T item);
    }
}

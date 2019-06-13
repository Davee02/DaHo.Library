using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaHo.Library.AspNetCore.Data.Repositories.Abstractions
{
    public interface IGenericInterface<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}

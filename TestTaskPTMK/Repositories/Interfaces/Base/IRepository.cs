using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTaskPTMK.Repositories.Interfaces.Base
{
    public interface IRepository<T>
    {
        public Task CreateAsync(T t);
        public Task<T> GetAsync(long id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task UpdateAsync(T t);
        public Task DeleteAsync(long id);
    }
}
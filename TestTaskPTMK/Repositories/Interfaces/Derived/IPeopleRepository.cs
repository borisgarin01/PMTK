using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskPTMK.Models;

namespace TestTaskPTMK.Repositories.Interfaces.Base
{
    public interface IPeopleRepository : IRepository<Person>
    {
        public Task<IEnumerable<Person>> GetOrderedAsync();
        public Task<IEnumerable<Person>> GetByLastName(string lastName);

    }
}
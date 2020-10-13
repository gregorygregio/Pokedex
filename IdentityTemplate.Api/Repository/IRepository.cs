using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Repository
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(string id);

        Task<string> Add(T entity);
        Task<int> Update(T entity);
        void Delete(Guid id);
    }
}

using Pokedex.Api.Entities;
using System.Threading.Tasks;

namespace Pokedex.Api.Repository
{
    public interface IRepositoryType : IRepository<Type>
    {

        Task<Type> GetByName(string name);
    }
}

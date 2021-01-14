using System.Collections.Generic;
using System.Threading.Tasks;
using PokedexAPI.Models;

namespace PokedexAPI.Repositories.Contracts
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> FindAllAsync();
        Task InsertAsync(Pokemon pokemon); 
    }
}
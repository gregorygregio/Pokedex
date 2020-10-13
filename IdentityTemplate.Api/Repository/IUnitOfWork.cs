using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Repository
{
    public interface IUnitOfWork
    {
        IRepositoryPokemon Pokemons { get; }
        IRepositoryType Types { get; }
        Task Commit();
        void Dispose();
    }
}

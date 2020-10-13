using IdentityTemplate.Api.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private RepositoryPokemon _pokemon;
        private RepositoryType _type;

        public UnitOfWork(IConfiguration configuration, DatabaseContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        public IRepositoryPokemon Pokemons
        => _pokemon ??= new RepositoryPokemon(_configuration);

        public IRepositoryType Types => _type ??= new RepositoryType(_configuration);

        public async Task Commit() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}

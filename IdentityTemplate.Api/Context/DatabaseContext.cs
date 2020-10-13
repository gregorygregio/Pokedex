
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pokedex.Api.Entities;

namespace IdentityTemplate.Api.Context
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext()
        {  }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public DbSet<Pokemon> Pokemons;
        public DbSet<Type> Types;
        public DbSet<Pokedex.Api.Entities.Type> Type { get; set; }
        
    }
}

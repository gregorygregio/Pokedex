using FluentValidation;
using PokedexAPI.Models;
using Dapper;
using System.Data.SqlClient;

namespace PokedexAPI.Helpers.Validators
{
    public class PokemonValidator : AbstractValidator<Pokemon>
    {
        public PokemonValidator()
        {
            RuleFor(x => x.PokedexIndex)
                .NotNull();
            
            RuleFor(x => x.Name)
                .Must(UniqueName)
                .Length(5, 26).NotNull();

            RuleFor(x => x.Hp)
                .InclusiveBetween(1, 255).NotNull();
            
            RuleFor(x => x.Attack)
                .InclusiveBetween(5, 190).NotNull();

            RuleFor(x => x.Defense)
                .InclusiveBetween(5, 230).NotNull();

            RuleFor(x => x.SpecialAttack)
                .InclusiveBetween(10, 194).NotNull();
            
            RuleFor(x => x.SpecialDefense)
                .InclusiveBetween(20, 230).NotNull();

            RuleFor(x => x.Speed)
                .InclusiveBetween(5, 180).NotNull();

            RuleFor(x => x.Generation)
                .InclusiveBetween(1, 6).NotNull();

            RuleForEach(x => x.Types)
                .SetValidator(new TypeValidator());
        }

        private bool UniqueName(Pokemon pokemon, string name)
        {
            using(var cnn = new SqlConnection("User ID=postgres;Password=Icaronon9;Host=localhost;Port=5432;Database=pokedexappdb;Pooling=true;"))
            {
                var dbPokemon = cnn.QuerySingle<Pokemon>("SELECT * FROM pokemons WHERE id = @id", new { id = pokemon.Id});

                if(dbPokemon == null)
                {
                    return true;
                }

                return dbPokemon.Id == pokemon.Id;
            }
        }
    }
}
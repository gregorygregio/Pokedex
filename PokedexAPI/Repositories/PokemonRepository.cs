using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using PokedexAPI.Models;
using PokedexAPI.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System.Text;
using System;
using System.Linq;

namespace PokedexAPI.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IConfiguration Config;

        public PokemonRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<Pokemon>> FindAllAsync()
        {
            using(var cnn = new SqlConnection(Config.GetConnectionString("PokedexAppDb")))
            {
                var query = new StringBuilder();

                query.Append("SELECT * FROM pokemons ");
                query.Append("SELECT * FROM pokemon_types ");
                query.Append("SELECT * FROM types");

                using(var results = await cnn.QueryMultipleAsync(query.ToString()))
                {
                    var pokemons = results.Read<Pokemon>().ToList();
                    var pokemonTypes = results.Read<PokemonTypes>().ToList();
                    var types = results.Read<PokedexAPI.Models.Type>().ToList();

                    foreach(var pokemon in pokemons)
                    {
                        var pokemonTypesSelected = pokemonTypes.Where(x => x.Id == pokemon.Id).ToList();
                        foreach(var pokemonType in pokemonTypesSelected)
                        {
                            var typesByPokemonId = types.Where(x => x.Id == pokemonType.TypeId).ToList();
                            pokemon.Types = typesByPokemonId;
                        }
                    }

                    return pokemons;
                }
            }
        }

        public async Task InsertAsync(Pokemon pokemon)
        {
            using(var cnn = new SqlConnection(Config.GetConnectionString("PokedexAppDb")))
            {
                await cnn.ExecuteAsync("INSERT INTO pokemons" +
	                    "id, pokedex_index, name, hp, attack, defense, special_attack, special_defense, speed, generation)" +
	                    "VALUES (@id ,@pokedex_index, @name, @hp, @attack, @defense, @special_attack, @special_defense, @speed, @generation", new {
                            id = pokemon.Id, 
                            pokedex_index = pokemon.PokedexIndex, 
                            name = pokemon.Name, 
                            hp = pokemon.Hp, 
                            attack = pokemon.Attack, 
                            defense = pokemon.Defense, 
                            special_attack = pokemon.SpecialAttack, 
                            special_defense = pokemon.SpecialDefense, 
                            speed = pokemon.Speed, 
                            generation = pokemon.Generation
                    });

                if(pokemon.Types.Count != 0)
                {
                    foreach(var type in pokemon.Types)
                    {
                        await cnn.ExecuteAsync("INSERT INTO pokemon_types(id, pokemon_id, type_id) VALUES (@id, @pokemon_id, @type_id)", new {
                            id = Guid.NewGuid().ToString(), pokemon_id = pokemon.Id, type_id = type.Id
                        });
                    }
                }
            }
        }
    }
}
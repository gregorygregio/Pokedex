using Dapper;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PokedexAPI.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexAPI.Models
{
    public class PokemonsRepository : IPokemonRepository
    {
        private readonly IConfiguration _config;

        public PokemonsRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_config.GetConnectionString("PostGreConnection"));
            }
        }

        public List<Pokemons> GetPokemons()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                string sql = @"SELECT pkmn.id, 
                                pkmn.pokedex_index, 
                                pkmn.name, 
                                pkmn.hp, 
                                pkmn.attack, 
                                pkmn.defense, 
                                pkmn.special_attack, 
                                pkmn.special_defense, 
                                pkmn.speed, 
                                pkmn.generation,
                                pktp.type_id as tipos_pokemon
                                FROM pokemons pkmn
                                left join pokemon_types pktp on pkmn.id = pktp.pokemon_id
                                order by pkmn.pokedex_index";

                var pokemonDictionary = new Dictionary<string, Pokemons>();

                var list = conn.Query<Pokemons, pokemon_types, Pokemons>(
                    sql,
                    (pokemon, pokemonTypes) =>
                    {
                        Pokemons pokemonEntry;

                        if (!pokemonDictionary.TryGetValue(pokemon.id, out pokemonEntry))
                        {
                            pokemonEntry = pokemon;
                            pokemonEntry.types = new List<pokemon_types>();
                            pokemonDictionary.Add(pokemonEntry.id, pokemonEntry);
                        }

                        pokemonEntry.types.Add(pokemonTypes);
                        return pokemonEntry;
                    }, splitOn: "id_pokemon,tipos_pokemon")
                    .Distinct()
                    .ToList();

                return list;

            }

        }

        public Microsoft.AspNetCore.Mvc.ActionResult<dynamic> InsertPokemon(Pokemons newPokemon)
        {

            BindingList<string> errors = new BindingList<string>();
            PokemonValidator validator = new PokemonValidator();
            ValidationResult results = validator.Validate(newPokemon); //valida o pokemon que tá chegando

            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    errors.Add($"{failure.PropertyName}: {failure.ErrorMessage}");
                }

                return errors;
            }

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                Guid pokeguid = Guid.NewGuid();
                int rowsAffected = conn.QueryFirst<int>(@"INSERT INTO pokemons(
                                                        id, 
                                                        name, 
                                                        hp,
                                                        attack,
                                                        defense,
                                                        special_attack,
                                                        special_defense,
                                                        speed,
                                                        generation)
                                                        VALUES(
                                                        @pokemonguid,
                                                        @name,
                                                        @hp,
                                                        @attack,
                                                        @defense,
                                                        @special_attack,
                                                        @special_defense,
                                                        @speed,
                                                        @generation)",
                    new
                    {                     
                        pokemonguid = pokeguid,
                        name = newPokemon.name,
                        hp = newPokemon.hp,
                        attack = newPokemon.attack,
                        defense = newPokemon.defense,
                        special_attack = newPokemon.special_attack,
                        special_defense = newPokemon.special_defense,
                        speed = newPokemon.speed,
                        generation = newPokemon.generation
                    });

                if (newPokemon.types != null && newPokemon.types.Count > 0)
                {
                    foreach (var types in newPokemon.types)
                    {
                        int rowsAffectedAddresses = conn.QueryFirst<int>(@"INSERT INTO pokemon_types(
                                                                            id, pokemon_id, type_id)
                                                                            VALUES (@guidtipo, @pokemonguid, @guidtipotabela);",
                            new
                            {
                                guidtipo = Guid.NewGuid(),
                                pokemonguid = pokeguid,
                                guidtipotabela = Guid.NewGuid() //não vou conseguir mostrar os tipos por tempo e não entender direito como ir na outra tabela...Sorry
                            });
                    }
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdatePokemon(Pokemons ourPokemon)
        {
            throw new NotImplementedException();
        }
        public bool DeletePokemon(int pokemonId)
        {
            throw new NotImplementedException();
        }
        public Pokemons GetSinglePokemon(int employeeId)
        {
            throw new NotImplementedException();
        }



    }
}

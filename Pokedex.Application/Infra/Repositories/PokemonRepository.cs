using Dapper;
using Pokedex.Application.Entities;
using Pokedex.Application.Infra.Database.Interfaces;
using Pokedex.Application.Infra.Repositories.Interfaces;
using Slapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Pokedex.Application.Infra.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IDatabase Db;

        public PokemonRepository(
            IDatabase db    
        )
        {
            Db = db;
        }

        public Pokemon Create(Pokemon data)
        {
            var sql = GetInsertPokemonSQL();

            var pokemon = Db.Connection.QuerySingle<Pokemon>(sql, new { 
                INDEX = data.pokedex_index,
                NAME = data.name,
                HP = data.hp,
                ATTACK = data.attack,
                DEFENSE = data.defense,
                SPATTACK = data.special_attack,
                SPDEFENSE = data.special_defense,
                SPEED = data.speed,
                GENERATION = data.generation
            });

            sql = "INSERT INTO pokemon_types(pokemon_id, type_id) VALUES(@POKEMONID, @TYPEID)";

            foreach(var type in data.Types)
            {
                Db.Connection.Execute(sql, new { POKEMONID = pokemon.id, TYPEID = type.id});
            }            

            return pokemon;
        }

        public IEnumerable<Pokemon> List()
        {
            var sql = GetListPokemonsSQL();

            var queryResult = Db.Connection.Query<dynamic>(sql);

            AutoMapper.Configuration.AddIdentifier(
                typeof(Pokemon), "pk.id"
            );

            AutoMapper.Configuration.AddIdentifier(
                typeof(PokemonType), "tp.id"
            );

            List<Pokemon> result = (AutoMapper.MapDynamic<Pokemon>(queryResult) as IEnumerable<Pokemon>).ToList();

            return result;
        }

        private string GetListPokemonsSQL()
        {
            return "SELECT * FROM pokemons AS pk " +
                "INNER JOIN pokemon_types AS pt ON pk.id = pt.pokemon_id " +
                "INNER JOIN types AS tp ON pt.type_id = tp.id";
        }

        private string GetInsertPokemonSQL()
        {
            return "INSERT INTO pokemons(" +
                    "pokedex_index, " +
                    "name, " +
                    "hp, " +
                    "attack, " +
                    "defense, " +
                    "special_attack, " +
                    "special_defense, " +
                    "speed, " +
                    "generation" +
                ")" +
                "VALUES(" +
                    "@INDEX," +
                    "@NAME," +
                    "@HP," +
                    "@ATTACK," +
                    "@DEFENSE," +
                    "@SPATTACK," +
                    "@SPDEFENSE," +
                    "@SPEED," +
                    "@GENERATION" +
                ") " +
                "RETURNING *";
        }
    }
}

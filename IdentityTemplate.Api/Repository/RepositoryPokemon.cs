using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Pokedex.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pokedex.Api.Repository
{
    public class RepositoryPokemon : IRepositoryPokemon
    {
        private readonly IConfiguration configuration;

        public RepositoryPokemon(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> Add(Pokemon entity)
        {
            string sql = @"INSERT INTO " +
                "Pokemons " +
                "(pokedex_index, name, hp, attack, defence, " +
                "special_attack, special_defence, speed, generation) " +
                "VALUES " +
                "(@pokedex_index, @name, @hp, @attack, @defence, " +
                "@special_attack, @special_defence, @speed, @generation); " +
                "RETURNING id";

            using var connection = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection))"));
            await connection.OpenAsync();

            return await connection.QueryAsync(sql, entity).Result.FirstOrDefault();

        }

        public async void Delete(Guid id)
        {
            string sql = "DELETE FROM Pokemons WHERE id = @id";

            using var connection = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection))"));
            await connection.OpenAsync();

            await connection.ExecuteAsync(sql, new { id });
        }

        public async Task<IEnumerable<Pokemon>> Get()
        {
            string sql = @"SELECT * FROM Pokemons";

            using var connection = new 
                NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            return await connection.QueryAsync<Pokemon>(sql);
        }

        public async Task<Pokemon> Get(string id)
        {
            string sql = @"SELECT * FROM Pokemons WHERE id = @id";

            using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var result = connection.QueryAsync<Pokemon>(sql, new { id = new Guid(id) }).Result.FirstOrDefault();

            return result;
        }

        public Task<Pokemon> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(Pokemon entity)
        {
            string sql = "UPDATE Pokemons SET " +
                "pokedex_index = @pokedex_index, " +
                "name = @name, " +
                "hp = @hp, " +
                "attack = @attack, " +
                "defence = @defence, " +
                "special_attack = @special_attack, " +
                "special_defence = @special_defence, " +
                "speed = @speed, " +
                "generation = @generation";

            using var connection = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection))"));
            await connection.OpenAsync();

            return await connection.ExecuteAsync(sql, entity);
        }
    }
}

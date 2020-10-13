using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Pokedex.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Repository
{
    public class RepositoryType : IRepositoryType
    {
        private readonly IConfiguration configuration;

        public RepositoryType(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> Add(Type entity)
        {
            string sql = @"INSERT INTO Types (type) VALUES (@type) RETURNING id";

            using var connection = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection))"));
            await connection.OpenAsync();

            return await connection.QueryAsync(sql, entity).Result.FirstOrDefault();

        }

        public async void Delete(System.Guid id)
        {
            string sql = "DELETE FROM Types WHERE id = @id";

            using var connection = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection))"));
            await connection.OpenAsync();

            await connection.ExecuteAsync(sql, new { id });
        }

        public async Task<IEnumerable<Type>> Get()
        {
            string sql = @"SELECT * FROM Types";

            using var connection = new
                NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            return await connection.QueryAsync<Type>(sql);
        }

        public async Task<Type> Get(string id)
        {
            string sql = @"SELECT * FROM Types WHERE id = @id";

            using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var result = connection.QueryAsync<Type>(sql, new { id = new System.Guid(id) }).Result.FirstOrDefault();

            return result;
        }

        public async Task<Type> GetByName(string name)
        {
            string sql = @"SELECT * FROM Types WHERE type = @type";

            using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var result = connection.QueryAsync<Type>(sql, new { type = name }).Result.FirstOrDefault();

            return result;
        }

        public async Task<int> Update(Type entity)
        {
            string sql = "UPDATE Types SET type = @type";

            using var connection = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection))"));
            await connection.OpenAsync();

            return await connection.ExecuteAsync(sql, entity);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Npgsql;
using Pokedex.Application.Infra.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Pokedex.Application.Infra.Database
{
    public class Database : IDatabase
    {
        public DbConnection Connection { get; set; }

        public Database(IConfiguration configuration)
        {
            Connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}

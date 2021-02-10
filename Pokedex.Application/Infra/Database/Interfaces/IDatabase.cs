using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Pokedex.Application.Infra.Database.Interfaces
{
    public interface IDatabase
    {
        public DbConnection Connection { get; set; }
    }
}

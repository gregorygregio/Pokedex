using Pokedex.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Application.Infra.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity<T>
    {
        T Create(T data);

        IEnumerable<T> List();
    }
}

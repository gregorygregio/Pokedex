using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexAPI.Models
{
    public class Type
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string type { get; set; }
    }
}

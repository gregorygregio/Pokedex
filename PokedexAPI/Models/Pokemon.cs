using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexAPI.Models
{
    public class Pokemon
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int PokedexIndex { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int Generation { get; set; }
        public int Total { get; set; }

        public virtual List<Type> Types { get; set; } = new List<Type>();

        public Pokemon()
        {
            Total = (Hp + Attack + Defense + SpecialAttack + SpecialDefense + Speed);
        }
    }
}

using System;

namespace PokedexAPI.Models
{
    public class PokemonTypes
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PokemonId { get; set; }
        public string TypeId { get; set; }
    }
}
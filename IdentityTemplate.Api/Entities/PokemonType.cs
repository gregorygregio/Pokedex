using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Entities
{
    public class PokemonType
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public Guid pokemon_id { get; set; }
        [Required]
        public Guid type_id { get; set; }
    }
}

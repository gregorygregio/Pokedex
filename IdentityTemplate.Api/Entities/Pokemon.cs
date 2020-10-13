using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Entities
{
    public class Pokemon
    {
        [Key]
        protected Guid id_guid { get; set; }

        public string id
        {
            get { return id_guid.ToString("N"); }
            set { id_guid = new Guid(value); }
        }

        public string Total()
        {
            return "";
        }

        [Required]
        public int pokedex_index { get; set; }
        [MaxLength(26), Required]
        public string name { get; set; }
        [Required, Range(1,255)]
        public int hp { get; set; }
        [Required, Range(5, 190)]
        public int attack { get; set; }
        [Required, Range(5, 230)]
        public int defense { get; set; }
        [Required, Range(10, 194)]
        public int special_attack { get; set; }
        [Required, Range(20, 230)]
        public int special_defense { get; set; }
        [Required, Range(5, 180)]
        public int speed { get; set; }
        [Required, Range(1, 6)]
        public int generation { get; set; }

    }
}

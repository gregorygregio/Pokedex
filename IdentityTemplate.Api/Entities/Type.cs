using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Entities
{
    public class Type
    {
        
        private Guid id_guid { get; set; }
        [Key]
        public string id
        {
            get { return id_guid.ToString("N"); }
            set { id_guid = new Guid(value); }
        }
        [MaxLength(36)]
        public string type { get; set; }
    }
}

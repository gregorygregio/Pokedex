using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexAPI.Models
{
    public class Pokemons
    {
        [Key]
        public string id { get; set; }
        public int pokedex_index { get; set; }
        public string name { get; set; }
        public int hp { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int special_attack { get; set; }
        public int special_defense { get; set; }
        public int speed { get; set; }
        public int generation { get; set; }

        public List<pokemon_types> types { get; set; }

    }

    public class pokemon_types
    {
        [Key]
        public string id { get; set; }
        public string pokemon_id { get; set; }
        public string type_id { get; set; }     

    }

    public interface IPokemonRepository
    {
        List<Pokemons> GetPokemons();
        Pokemons GetSinglePokemon(int employeeId);
        ActionResult<dynamic> InsertPokemon(Pokemons newPokemon);
        bool DeletePokemon(int pokemonId);
        bool UpdatePokemon(Pokemons ourPokemon);
    }













}

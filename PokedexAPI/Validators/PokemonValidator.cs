using FluentValidation;
using PokedexAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexAPI.Validators
{
    public class PokemonValidator : AbstractValidator<Pokemons> 
    {
        public PokemonValidator()
        {
            RuleFor(e => e.name).NotEmpty();
        }


    }
}

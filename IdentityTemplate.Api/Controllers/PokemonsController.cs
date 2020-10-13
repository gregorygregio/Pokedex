using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityTemplate.Api.Context;
using Pokedex.Api.Entities;
using Pokedex.Api.Repository;

namespace Pokedex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public PokemonsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Pokemons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetPokemon()
        {
            var result = await _uow.Pokemons.Get();
            return new ActionResult<IEnumerable<Pokemon>>(result);
        }

        // GET: api/Pokemons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(string id)
        {
            var pokemon = await _uow.Pokemons.Get(id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return pokemon;
        }

        // PUT: api/Pokemons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokemon(string id, Pokemon pokemon)
        {
            if (id != pokemon.id)
            {
                return BadRequest();
            }

            await _uow.Pokemons.Update(pokemon);

            try
            {
                await _uow.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pokemons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pokemon>> PostPokemon(Pokemon pokemon)
        {
            await _uow.Pokemons.Add(pokemon);
            await _uow.Commit();

            return await GetPokemon(pokemon.id);
        }

        // DELETE: api/Pokemons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pokemon>> DeletePokemon(string id)
        {
            var pokemon = await _uow.Pokemons.Get(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            _uow.Pokemons.Delete(new Guid(id));
            await _uow.Commit();

            return pokemon;
        }

        private bool PokemonExists(string id)
        {
            return _uow.Pokemons.Get(id) != null;
        }

        private bool TypeExists(string name)
        {
            return _uow.Types.GetByName(name) != null;
        }
    }
}

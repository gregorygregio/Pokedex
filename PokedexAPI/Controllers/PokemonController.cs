using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PokedexAPI.Models;
using PokedexAPI.Repositories.Contracts;

namespace PokedexAPI.Controllers
{
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository Repo;
        private readonly IConfiguration Config;

        public PokemonController(IConfiguration config, IPokemonRepository repo)
        {
            Repo = repo;
            Config = config;
        }

        [HttpGet("", Name="FindAllPokemons")]
        public async Task<IActionResult> FindAllPokemons()
        {
            var results = await Repo.FindAllAsync();

            if(results.Count == 0)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [Authorize]
        [HttpPost("", Name="InsertPokemon")]
        public async Task<IActionResult> InsertPokemon([FromBody]Pokemon pokemon)
        {
            if(pokemon == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return UnprocessableEntity();
            }

            await Repo.InsertAsync(pokemon);

            return Created($"api/[controller]/{pokemon.Id}", pokemon);
        }
    }
}
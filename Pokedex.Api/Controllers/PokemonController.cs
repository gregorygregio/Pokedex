using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.Application.Entities;
using Pokedex.Application.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [ApiController]
    [Route("pokemons")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        //[Authorize(Roles = "trainer")]
        public ActionResult<Pokemon> Post(
            [FromBody] Pokemon pokemon,
            [FromServices] IPokemonRepository repository
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!pokemon.IsValid())
                return BadRequest(pokemon.ValidationResult);

            return Ok(repository.Create(pokemon));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Pokemon>> Get(
            [FromServices] IPokemonRepository repository   
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(repository.List());
        }
    }
}

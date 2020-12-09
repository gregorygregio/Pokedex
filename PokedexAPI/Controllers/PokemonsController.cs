using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokedexAPI.Models;
using PokedexAPI.Repositories;
using PokedexAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokedexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private IPokemonRepository _ourPokemonRepository;

        public PokemonsController(IPokemonRepository pokemonRepository) //injeção do repositório
        {
            _ourPokemonRepository = pokemonRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model) //login simples para conseguir o token
        {
            try
            {
                var user = UserRepository.Get(model.username, model.password);
                if (user == null)
                    return BadRequest(new { message = "Usuário ou senha inválidos" });
                var token = TokenService.GenerateToken(user);
                user.password = "";
                return new
                {
                    user = user,
                    token = token
                };

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/<PokemonsController>
        [HttpGet]
        [AllowAnonymous]
        public string Get()
        {
            var pokemons = _ourPokemonRepository.GetPokemons();
            return JsonConvert.SerializeObject(pokemons);
        }

        // POST api/<PokemonsController>
        [HttpPost]
        [Authorize(Roles = "trainer,gymleader")] //cadastrar pokemon somente se tiver as roles listadas
        public ActionResult<dynamic> Post([FromBody] Pokemons pokemon)
        {
            var retorno = _ourPokemonRepository.InsertPokemon(pokemon);
            return JsonConvert.SerializeObject(retorno);
        }

    }
}

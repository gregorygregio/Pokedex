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
    public class TypesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public TypesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Types
        [HttpGet]
        public new async Task<ActionResult<IEnumerable<Type>>> GetType()
        {
            var result = await _uow.Types.Get();
            return new ActionResult<IEnumerable<Type>>(result);
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Type>> GetType(string id)
        {
            var @type = await _uow.Types.Get(id);

            if (@type == null)
            {
                return NotFound();
            }

            return @type;
        }

        // PUT: api/Types/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(string id, Type type)
        {
            if (id != type.id)
            {
                return BadRequest();
            }

            await _uow.Types.Add(type);

            try
            {
                await _uow.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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

        // POST: api/Types
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Type>> PostType(Type type)
        {
            await _uow.Types.Add(type);
            await _uow.Commit();

            return CreatedAtAction("GetType", new { id = new System.Guid(type.id) }, type);
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Type>> DeleteType(string id)
        {
            var type = await _uow.Types.Get(id);
            if (type == null)
            {
                return NotFound();
            }

            _uow.Types.Delete(new System.Guid(id));
            await _uow.Commit();

            return @type;
        }

        private bool TypeExists(string id)
        {
            return _uow.Types.Get(id) != null;
        }
    }
}

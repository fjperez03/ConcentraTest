using AutoMapper;
using ConcentraTest.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ConcentraTest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ConcentraContext _context;

        public ClientsController(ConcentraContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOClient>>> GetClients()
        {
            List<DTOClient> client = null;
          if (_context.Clients == null)
          {
              return NotFound();
          }
            client = _mapper.Map<List<Client>, List<DTOClient>>(await _context.Clients.ToListAsync());
            return client;
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOClient>> GetClient(string id)
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            var client = _mapper.Map<Client,DTOClient>(await _context.Clients.FindAsync(id));

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(string id, DTOClient dtoClient)
        {
            string patternName = @"(\w|\s|\.){1,50}";

            if (!Regex.IsMatch(dtoClient.Name, patternName) | !Regex.IsMatch(dtoClient.LastName, patternName))
            {
                return BadRequest("Por favor verificar nombres y/o apellidos, existen caracteres Invalidos.");
            }

            Client client = _mapper.Map<DTOClient, Client>(dtoClient);
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DTOClient>> PostClient(DTOClient dtoClient)
        {
            string patternCedula = @"^(\d{11}|\d{9})$";
            string patternName = @"(\w|\s|\.){1,50}";

            if(!Regex.IsMatch(dtoClient.ClientId, patternCedula))
            {
                return BadRequest("Identificación Invalida, La identificación debe ser de 11 digitos para persona Fisica o 9 Para persona Juridica, sin guiones ni espacios.");
            }

            if (!Regex.IsMatch(dtoClient.Name, patternName) | !Regex.IsMatch(dtoClient.LastName, patternName))
            {
                return BadRequest("Por favor verificar nombres y/o apellidos, existen caracteres Invalidos.");
            }

            Client client = _mapper.Map<DTOClient, Client>(dtoClient);  
          if (_context.Clients == null)
          {
              return Problem("Entity set 'ConcentraContext.Clients'  is null.");
          }
            _context.Clients.Add(client);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(client.ClientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClient", new { id = dtoClient.ClientId }, dtoClient);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(string id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}

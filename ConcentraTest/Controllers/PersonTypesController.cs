using AutoMapper;
using ConcentraTest.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcentraTest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonTypesController : ControllerBase
    {
        private IMapper _mapper;
        private readonly ConcentraContext _context;

        public PersonTypesController(ConcentraContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/PersonTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOPersonType>>> GetPersonTypes()
        {
          if (_context.PersonTypes == null)
          {
              return NotFound();
          }
            return _mapper.Map<List<PersonType>, List<DTOPersonType>>(await _context.PersonTypes.ToListAsync());
        }

        // GET: api/PersonTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOPersonType>> GetPersonType(int id)
        {
          if (_context.PersonTypes == null)
          {
              return NotFound();
          }
            var personType = _mapper.Map<PersonType, DTOPersonType>(await _context.PersonTypes.FindAsync(id));

            if (personType == null)
            {
                return NotFound();
            }

            return personType;
        }

        // PUT: api/PersonTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonType(int id, DTOPersonType dtoPersonType)
        {
            PersonType personType = _mapper.Map<DTOPersonType, PersonType>(dtoPersonType);
            if (id != personType.PersonTypeId)
            {
                return BadRequest();
            }

            _context.Entry(personType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonTypeExists(id))
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

        // POST: api/PersonTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonType>> PostPersonType(DTOPersonType dtoPersonType)
        {
            PersonType personType = _mapper.Map<DTOPersonType, PersonType>(dtoPersonType);
            if (_context.PersonTypes == null)
          {
              return Problem("Entity set 'ConcentraContext.PersonTypes'  is null.");
          }
            _context.PersonTypes.Add(personType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonType", new { id = dtoPersonType.PersonTypeId }, dtoPersonType);
        }

        // DELETE: api/PersonTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonType(int id)
        {
            if (_context.PersonTypes == null)
            {
                return NotFound();
            }
            var personType = await _context.PersonTypes.FindAsync(id);
            if (personType == null)
            {
                return NotFound();
            }

            _context.PersonTypes.Remove(personType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonTypeExists(int id)
        {
            return (_context.PersonTypes?.Any(e => e.PersonTypeId == id)).GetValueOrDefault();
        }
    }
}

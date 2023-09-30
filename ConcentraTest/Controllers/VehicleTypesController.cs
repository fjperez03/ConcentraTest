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
    public class VehicleTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ConcentraContext _context;

        public VehicleTypesController(ConcentraContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/VehicleTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOVehicleType>>> GetVehicleTypes()
        {
          if (_context.VehicleTypes == null)
          {
              return NotFound();
          }
            return _mapper.Map<List<VehicleType>, List<DTOVehicleType>>(await _context.VehicleTypes.ToListAsync());
        }

        // GET: api/VehicleTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOVehicleType>> GetVehicleType(int id)
        {
          if (_context.VehicleTypes == null)
          {
              return NotFound();
          }
            var vehicleType = _mapper.Map<VehicleType, DTOVehicleType>(await _context.VehicleTypes.FindAsync(id));

            if (vehicleType == null)
            {
                return NotFound();
            }

            return vehicleType;
        }

        // PUT: api/VehicleTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleType(int id, DTOVehicleType dtoVehicleType)
        {
            VehicleType vehicleType = _mapper.Map<DTOVehicleType, VehicleType>(dtoVehicleType);
            if (id != vehicleType.VehicleTypeId)
            {
                return BadRequest();
            }

            _context.Entry(vehicleType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleTypeExists(id))
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

        // POST: api/VehicleTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleType>> PostVehicleType(DTOVehicleType dtoVehicleType)
        {
            VehicleType vehicleType = _mapper.Map<DTOVehicleType, VehicleType>(dtoVehicleType);
            if (_context.VehicleTypes == null)
          {
              return Problem("Entity set 'ConcentraContext.VehicleTypes'  is null.");
          }
            _context.VehicleTypes.Add(vehicleType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleType", new { id = dtoVehicleType.VehicleTypeId }, dtoVehicleType);
        }

        // DELETE: api/VehicleTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType(int id)
        {
            if (_context.VehicleTypes == null)
            {
                return NotFound();
            }
            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            _context.VehicleTypes.Remove(vehicleType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleTypeExists(int id)
        {
            return (_context.VehicleTypes?.Any(e => e.VehicleTypeId == id)).GetValueOrDefault();
        }
    }
}

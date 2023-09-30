using AutoMapper;
using ConcentraTest.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ConcentraTest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PlatesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ConcentraContext _context;

        public PlatesController(ConcentraContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Plates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOPlate>>> GetPlates()
        {
          if (_context.Plates == null)
          {
              return NotFound();
          }
            return _mapper.Map<List<Plate>, List<DTOPlate>>(await _context.Plates.ToListAsync());
        }

        // GET: api/Plates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOPlate>> GetPlate(string id)
        {
          if (_context.Plates == null)
          {
              return NotFound();
          }
            var plate = _mapper.Map<Plate, DTOPlate>(await _context.Plates.FindAsync(id));

            if (plate == null)
            {
                return NotFound();
            }

            return plate;
        }

        // PUT: api/Plates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlate(string id, DTOPlate dtoPlate)
        {
            Plate plate = _mapper.Map<DTOPlate, Plate>(dtoPlate);
            if (id != plate.PlateId)
            {
                return BadRequest();
            }

            _context.Entry(plate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlateExists(id))
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
        */

        // POST: api/Plates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlateResponse>> PostPlate([FromBody] PlateArgs args)
        {
            PlateResponse myPlate = null;
            if (_context.Plates == null)
          {
              return Problem("Entity set 'ConcentraContext.Plates'  is null.");
          }

            try
            {
                using(SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "USP_GenerateUniquePlate";
                        cmd.Parameters.AddWithValue("@VehicleID", args.vehicleID);
                        cmd.Parameters.AddWithValue("@UserID", args.userID);

                        await con.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                myPlate = new PlateResponse();
                                while (await reader.ReadAsync())
                                {
                                    myPlate.PlateId = !await reader.IsDBNullAsync(reader.GetOrdinal("PlateID")) ? reader.GetString(reader.GetOrdinal("PlateID")) : String.Empty;
                                    myPlate.Price = !await reader.IsDBNullAsync(reader.GetOrdinal("Price")) ? reader.GetDecimal(reader.GetOrdinal("Price")) : 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (PlateExists(myPlate.PlateId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlate", new { id = myPlate.PlateId }, myPlate);
        }

        // DELETE: api/Plates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlate(string id)
        {
            if (_context.Plates == null)
            {
                return NotFound();
            }
            var plate = await _context.Plates.FindAsync(id);
            if (plate == null)
            {
                return NotFound();
            }

            _context.Plates.Remove(plate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlateExists(string id)
        {
            return (_context.Plates?.Any(e => e.PlateId == id)).GetValueOrDefault();
        }
    }
}

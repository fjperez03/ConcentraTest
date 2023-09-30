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
    public class ModelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ConcentraContext _context;

        public ModelsController(ConcentraContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Models
        [HttpGet]
        public async Task<ActionResult<List<DTOModel>>> GetModels()
        {
          if (_context.Models == null)
          {
              return NotFound();
          }
            return _mapper.Map<List<Model>, List<DTOModel>>(await _context.Models.ToListAsync());
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOModel>> GetModel(int id)
        {
          if (_context.Models == null)
          {
              return NotFound();
          }
            var model = _mapper.Map<Model, DTOModel>(await _context.Models.FindAsync(id));

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        // PUT: api/Models/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel(int id, DTOModel dtoModel)
        {
            Model model = _mapper.Map<DTOModel, Model>(dtoModel);
            if (id != model.ModelId)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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

        // POST: api/Models
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model>> PostModel(DTOModel dtoModel)
        {
            Model model = _mapper.Map<DTOModel, Model>(dtoModel);
            if (_context.Models == null)
          {
              return Problem("Entity set 'ConcentraContext.Models'  is null.");
          }
            _context.Models.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = dtoModel.ModelId }, dtoModel);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            if (_context.Models == null)
            {
                return NotFound();
            }
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelExists(int id)
        {
            return (_context.Models?.Any(e => e.ModelId == id)).GetValueOrDefault();
        }
    }
}

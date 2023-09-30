using AutoMapper;
using ConcentraTest.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ConcentraTest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ConcentraContext _context;

        public BrandsController(ConcentraContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<List<DTOBrand>>> GetBrands()
        {
            List<DTOBrand> brand = null;
            if (_context.Brands == null)
          {
              return NotFound();
          }
            brand = _mapper.Map<List<Brand>, List<DTOBrand>>(await _context.Brands.ToListAsync());
            return brand;
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOBrand>> GetBrand(int id)
        {
          if (_context.Brands == null)
          {
              return NotFound();
          }
            var brand = _mapper.Map<Brand, DTOBrand>(await _context.Brands.FindAsync(id));

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, DTOBrand dtoBrand)
        {
            Brand brand = _mapper.Map<DTOBrand, Brand>(dtoBrand);
            if (id != brand.BrandId)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(DTOBrand dtoBrand)
        {
            Brand brand = _mapper.Map<DTOBrand, Brand>(dtoBrand);
          if (_context.Brands == null)
          {
              return Problem("Entity set 'ConcentraContext.Brands'  is null.");
          }
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = dtoBrand.BrandId }, dtoBrand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return (_context.Brands?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Domain.Entities;

namespace ScratchBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubdivisionsController : ControllerBase
    {
        private readonly ScratchContext _context;

        public SubdivisionsController(ScratchContext context)
        {
            _context = context;
        }

        // GET: api/Subdivisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subdivision>>> GetSubdivision()
        {
            return await _context.Subdivision.ToListAsync();
        }

        // GET: api/Subdivisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subdivision>> GetSubdivision(int id)
        {
            var subdivision = await _context.Subdivision.FindAsync(id);

            if (subdivision == null)
            {
                return NotFound();
            }

            return subdivision;
        }

        // PUT: api/Subdivisions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubdivision(int id, Subdivision subdivision)
        {
            if (id != subdivision.Id)
            {
                return BadRequest();
            }

            _context.Entry(subdivision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubdivisionExists(id))
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

        // POST: api/Subdivisions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Subdivision>> PostSubdivision(Subdivision subdivision)
        {
            _context.Subdivision.Add(subdivision);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSubdivision", new { id = subdivision.Id }, subdivision);
        }

        // DELETE: api/Subdivisions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subdivision>> DeleteSubdivision(int id)
        {
            var subdivision = await _context.Subdivision.FindAsync(id);
            if (subdivision == null)
            {
                return NotFound();
            }

            _context.Subdivision.Remove(subdivision);
            await _context.SaveChangesAsync();

            return subdivision;
        }

        private bool SubdivisionExists(int id)
        {
            return _context.Subdivision.Any(e => e.Id == id);
        }
    }
}

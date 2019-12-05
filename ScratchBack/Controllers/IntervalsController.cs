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
    public class IntervalsController : ControllerBase
    {
        private readonly ScratchContext _context;

        public IntervalsController(ScratchContext context)
        {
            _context = context;
        }

        // GET: api/Intervals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interval>>> GetInterval()
        {
            return await _context.Interval.ToListAsync();
        }

        // GET: api/Intervals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interval>> GetInterval(int id)
        {
            var interval = await _context.Interval.FindAsync(id);

            if (interval == null)
            {
                return NotFound();
            }

            return interval;
        }

        // PUT: api/Intervals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterval(int id, Interval interval)
        {
            if (id != interval.Id)
            {
                return BadRequest();
            }

            _context.Entry(interval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntervalExists(id))
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

        // POST: api/Intervals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Interval>> PostInterval(Interval interval)
        {
            _context.Interval.Add(interval);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterval", new { id = interval.Id }, interval);
        }

        // DELETE: api/Intervals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Interval>> DeleteInterval(int id)
        {
            var interval = await _context.Interval.FindAsync(id);
            if (interval == null)
            {
                return NotFound();
            }

            _context.Interval.Remove(interval);
            await _context.SaveChangesAsync();

            return interval;
        }

        private bool IntervalExists(int id)
        {
            return _context.Interval.Any(e => e.Id == id);
        }
    }
}

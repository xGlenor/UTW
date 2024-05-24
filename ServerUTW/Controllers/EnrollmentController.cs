using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Models;
using ServerUTW.Data;

namespace ServerUTW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrolllment>>> GetEnrolllments()
        {
            return await _context.Enrolllments.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrolllment>> GetEnrolllment(int id)
        {
            var enrolllment = await _context.Enrolllments.FindAsync(id);

            if (enrolllment == null)
            {
                return NotFound();
            }

            return enrolllment;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrolllment(int id, Enrolllment enrolllment)
        {
            if (id != enrolllment.Id)
            {
                return BadRequest();
            }

            _context.Entry(enrolllment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrolllmentExists(id))
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


        [HttpPost]
        public async Task<ActionResult<Enrolllment>> PostEnrolllment(Enrolllment enrolllment)
        {
            _context.Enrolllments.Add(enrolllment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrolllment", new { id = enrolllment.Id }, enrolllment);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrolllment(int id)
        {
            var enrolllment = await _context.Enrolllments.FindAsync(id);
            if (enrolllment == null)
            {
                return NotFound();
            }

            _context.Enrolllments.Remove(enrolllment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnrolllmentExists(int id)
        {
            return _context.Enrolllments.Any(e => e.Id == id);
        }
    }
}

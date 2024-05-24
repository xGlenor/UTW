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
    public class FeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Fees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fee>>> GetFees()
        {
            return await _context.Fees.ToListAsync();
        }

        // GET: api/Fees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fee>> GetFee(int id)
        {
            var fee = await _context.Fees.FindAsync(id);

            if (fee == null)
            {
                return NotFound();
            }

            return fee;
        }

        // PUT: api/Fees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFee(int id, Fee fee)
        {
            if (id != fee.Id)
            {
                return BadRequest();
            }

            _context.Entry(fee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeExists(id))
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

        // POST: api/Fees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fee>> PostFee(Fee fee)
        {
            _context.Fees.Add(fee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFee", new { id = fee.Id }, fee);
        }

        // DELETE: api/Fees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFee(int id)
        {
            var fee = await _context.Fees.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }

            _context.Fees.Remove(fee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeeExists(int id)
        {
            return _context.Fees.Any(e => e.Id == id);
        }
    }
}

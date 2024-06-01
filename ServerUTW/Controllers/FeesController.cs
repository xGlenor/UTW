using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Models;
using ServerUTW.Data;
using BaseLibrary.Contracts;

namespace ServerUTW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly IFeeRepository _repository;

        public FeesController(IFeeRepository context)
        {
            _repository = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fee>>> GetFees()
        {
            return await _repository.GetAll();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Fee>> GetFee(int id)
        {
            var fee = await _repository.GetById(id);

            if (fee == null)
            {
                return NotFound();
            }

            return fee;
        }

        
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

        [HttpPost]
        public async Task<ActionResult<Fee>> PostFee(Fee fee)
        {
            var FeeAdded = await _repository.Insert(fee);

            return CreatedAtAction("GetFee", new { id = fee.Id }, fee);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFee(int id)
        {
            var fee = await _repository.GetById(id);
            if (fee == null)
            {
                return NotFound();
            }

            var feeDeleted = await _repository.Delete(fee.Id);

            return Ok(new { Message = "Pomyślnie usunięto opłate", Fee = feeDeleted });
        }

        
    }
}

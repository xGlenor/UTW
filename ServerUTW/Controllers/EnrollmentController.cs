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
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentController(IEnrollmentRepository context)
        {
            _repository = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrolllment>>> GetEnrolllments()
        {
            return await _repository.GetAll();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrolllment>> GetEnrolllment(int id)
        {
            var enrolllment = await _repository.GetById(id);

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
           var enrollmentAdded = await _repository.Insert(enrolllment);

            return CreatedAtAction("GetEnrolllment", new { id = enrolllment.Id }, enrolllment);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrolllment(int id)
        {
            var enrollment = await _repository.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentDeleted = await _repository.Delete(enrollment.Id);

            return Ok(new { Message = "Pomyœlnie usuniêto zapis", Enrollment = enrollmentDeleted });
        }

        
    }
}

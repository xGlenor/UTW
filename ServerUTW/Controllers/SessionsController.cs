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
    public class SessionsController : ControllerBase
    {
        private readonly ISessionRepository _repository;

        public SessionsController(ISessionRepository context)
        {
            _repository = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            return await _repository.GetAll();
        }

   
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            var session = await _repository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(int id, Session session)
        {
            if (id != session.Id)
            {
                return BadRequest();
            }

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
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
        public async Task<ActionResult<Session>> PostSession(Session session)
        {
            var sessionAdded = await _repository.Insert(session);

            return CreatedAtAction("GetSession", new { id = session.Id }, session);
        }

 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _repository.GetById(id);
            if (session == null)
            {
                return NotFound();
            }

            var sessionDeleted = await _repository.Delete(session.Id);

            return Ok(new { Message = "Pomyślnie usunięto semestr", Session = sessionDeleted });
        }

        
    }
}

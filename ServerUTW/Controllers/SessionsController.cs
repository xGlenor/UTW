using BaseLibrary.Contracts;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ServerUTW.Controllers;

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

        if (session == null) return NotFound();

        return session;
    }


    [HttpPut("{id:int}")]
    public async Task<EntityResponse> PutStudent(int id, Session session)
    {
        if (id != session.Id) return new EntityResponse(false, "Bad ID", null);

        var result = await _repository.Update(id, session);

        return new EntityResponse(true, "Updated successfully", Generics.SerializeObj(result));
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
        if (session == null) return NotFound();

        var sessionDeleted = await _repository.Delete(session.Id);

        return Ok(new { Message = "Pomyślnie usunięto semestr", Session = sessionDeleted });
    }
}
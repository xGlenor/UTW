using System.Diagnostics;
using AutoMapper;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.GenericModels;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerUTW.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _repository;

    public StudentsController(IStudentRepository context, IMapper mapper)
    {
        _repository = context;
        _mapper = mapper;
    }

    // GET: api/Students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _repository.GetAll();
    }

    [HttpGet("Enrolled")]
    public async Task<ActionResult<IEnumerable<Student>>> GetEnrolled()
    {
        return await _repository.GetStudents();
    }

    [HttpGet("Candidates")]
    public async Task<ActionResult<IEnumerable<Student>>> GetCandidates()
    {
        return await _repository.GetCandidates();
    }

    // GET: api/Students/5
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _repository.GetById(id);

        if (student == null) return NotFound();

        return student;
    }

    // POST: api/Students
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Student>> PostStudent(StudentDTO studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);
        var studentAdded = await _repository.Insert(student);

        return Ok(studentAdded);
    }

    [HttpPut("{id:int}")]
    public async Task<EntityResponse> PutStudent(int id, StudentDTO studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);
        var result = await _repository.Update(id, student);

        return new EntityResponse(true, "Updated successfully", Generics.SerializeObj(result));
    }

    // DELETE: api/Students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _repository.GetById(id);
        if (student == null) return NotFound();

        var studentDeleted = await _repository.Delete(student.Id);

        return Ok(new { Message = "Pomyślnie usunięto użytkownika", Student = studentDeleted });
    }
}
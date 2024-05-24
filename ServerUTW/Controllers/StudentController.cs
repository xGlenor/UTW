using BaseLibrary.Contracts;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServerUTW.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private static readonly Student[] Students = new[]
    {
        new Student() {FirstName = "Grzes", LastName = "Duraj"},
        new Student() {FirstName = "Patryk", LastName = "Cwajna"},
        new Student() {FirstName = "Bruno", LastName = "Howaniec"},
        new Student() {FirstName = "Kamil", LastName = "Dwornik"},
    };

    private readonly ILogger<StudentController> _logger;
    private readonly IStudentRepository _studentRepository;

    public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository)
    {
        _logger = logger;
        _studentRepository = studentRepository;
    }
    
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        return NotFound();
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IEnumerable<Student> GetStudentsByAdmin()
    {
        return Students;
    }
    
    [HttpGet("teacher")]
    [Authorize(Roles = "Teacher")]
    public IEnumerable<Student> GetStudentsByUser()
    {
        return Students;
    }
    
    
}
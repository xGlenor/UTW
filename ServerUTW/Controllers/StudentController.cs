using BaseLibrary.Models;
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

    public StudentController(ILogger<StudentController> logger)
    {
        _logger = logger;
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
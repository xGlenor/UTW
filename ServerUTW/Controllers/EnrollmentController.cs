using System.Diagnostics;
using AutoMapper;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ServerUTW.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EnrollmentController : ControllerBase
{
    private readonly ILessonRepository _lesson;
    private readonly IMapper _mapper;
    private readonly IEnrollmentRepository _repository;
    private readonly IStudentRepository _student;
    private readonly UserManager<ApplicationUser> _userManager;

    public EnrollmentController(IEnrollmentRepository context, IMapper mapper, ILessonRepository lesson,
        IStudentRepository student, UserManager<ApplicationUser> userManager)
    {
        _repository = context;
        _mapper = mapper;
        _lesson = lesson;
        _student = student;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enrolllment>>> GetEnrolllments()
    {
        return await _repository.GetAll();
    }

    [HttpGet("ByLesson/{lessonId}")]
    public async Task<ActionResult<IEnumerable<Enrolllment>>> GetStudentsByLessons(int lessonId)
    {
        return await _repository.GetStudentbyLessonId(lessonId);
    }

    [HttpGet("Teachers")]
    public async Task<ActionResult<IEnumerable<TeacherEnrollment>>> GetTeacherEnrollments()
    {
        return await _repository.GetAllTeachers();
    }

    [HttpGet("TeacherEnrollemntsbyId/{userId}")]
    public async Task<ActionResult<IEnumerable<TeacherEnrollment>>> GetTeacherEnrollments(string userId)
    {
        return await _repository.GetTeacherEnrollmentsById(userId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Enrolllment>> GetEnrolllment(int id)
    {
        var enrolllment = await _repository.GetById(id);

        if (enrolllment == null) return NotFound();

        return enrolllment;
    }

    [HttpGet("Teachers/{id}")]
    public async Task<ActionResult<TeacherEnrollment>> GetTeacherById(int id)
    {
        var enrolllment = await _repository.GetTeacherById(id);

        if (enrolllment == null) return NotFound();

        return enrolllment;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Enrolllment>> PutStudent(int id, EnrollmentDTO enrollmentDto)
    {
        var enrolllment = _mapper.Map<Enrolllment>(enrollmentDto);
        enrolllment.Student = await _student.GetById(enrolllment.StudentId);
        enrolllment.Lesson = await _lesson.GetById(enrolllment.LessonId);
        var result = await _repository.Update(id, enrolllment);

        return Ok(result);
    }

    [HttpPut("Teachers/{id:int}")]
    public async Task<ActionResult<TeacherEnrollment>> PutTeacher(int id, TeacherEnrollmentDto enrollmentDto)
    {
        var enrolllment = _mapper.Map<TeacherEnrollment>(enrollmentDto);
        enrolllment.Teacher = await _userManager.FindByIdAsync(enrolllment.TeacherId);
        enrolllment.Lesson = await _lesson.GetById(enrolllment.LessonId);
        var result = await _repository.UpdateTeacher(id, enrolllment);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Enrolllment>> PostEnrolllment(EnrollmentDTO enrolllmentDto)
    {
        var enrolllment = _mapper.Map<Enrolllment>(enrolllmentDto);
        enrolllment.Student = await _student.GetById(enrolllment.StudentId);
        enrolllment.Lesson = await _lesson.GetById(enrolllment.LessonId);
        var enrollmentAdded = await _repository.Insert(enrolllment);

        return Ok(enrollmentAdded);
    }

    [HttpPost("Teachers")]
    public async Task<ActionResult<TeacherEnrollment>> PostTeacherEnrolllment(TeacherEnrollmentDto enrolllmentDto)
    {
        var enrolllment = _mapper.Map<TeacherEnrollment>(enrolllmentDto);
        enrolllment.Teacher = await _userManager.FindByIdAsync(enrolllment.TeacherId);
        enrolllment.Lesson = await _lesson.GetById(enrolllment.LessonId);
        var enrollmentAdded = await _repository.InsertTeacher(enrolllment);

        return Ok(enrollmentAdded);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnrolllment(int id)
    {
        var enrollment = await _repository.GetById(id);
        if (enrollment == null) return NotFound();

        var enrollmentDeleted = await _repository.Delete(enrollment.Id);

        return Ok(new { Message = "Pomyślnie usunięto zapis", Enrollment = enrollmentDeleted });
    }

    [HttpDelete("Teachers/{id}")]
    public async Task<IActionResult> DeleteTeacherEnrolllment(int id)
    {
        var enrollment = await _repository.GetTeacherById(id);
        if (enrollment == null) return NotFound();

        var enrollmentDeleted = await _repository.DeleteTeacher(enrollment.Id);

        return Ok(new { Message = "Pomyślnie usunięto zapis", TeacherEnrollment = enrollmentDeleted });
    }
}
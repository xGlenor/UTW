using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Models;
using ServerUTW.Data;
using BaseLibrary.Contracts;
using BaseLibrary.DTOs;
using BaseLibrary.GenericModels;
using BaseLibrary.Responses;

namespace ServerUTW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _repository;
        private readonly ILessonRepository _lesson;
        private readonly IStudentRepository _student;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollmentRepository context, IMapper mapper, ILessonRepository lesson, IStudentRepository student)
        {
            _repository = context;
            _mapper = mapper;
            _lesson = lesson;
            _student = student;
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
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Enrolllment>> PutStudent(int id, EnrollmentDTO enrollmentDto)
        {
            var enrolllment = _mapper.Map<Enrolllment>(enrollmentDto);
            enrolllment.Student = await _student.GetById(enrolllment.StudentId);
            enrolllment.Lesson = await _lesson.GetById(enrolllment.LessonId);
            var result = await _repository.Update(id, enrolllment);

            return Ok(result);
        }
        

        [HttpPost]
        public async Task<ActionResult<Enrolllment>> PostEnrolllment(EnrollmentDTO enrolllmentDto)
        { 
            var enrolllment = _mapper.Map<Enrolllment>(enrolllmentDto);
            enrolllment.Student = await _student.GetById(enrolllment.StudentId);
            enrolllment.Lesson = await _lesson.GetById(enrolllment.LessonId);
            var enrollmentAdded = await _repository.Insert(enrolllment);

            return Ok();
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

            return Ok(new { Message = "Pomyślnie usunięto zapis", Enrollment = enrollmentDeleted });
        }

        
    }
}

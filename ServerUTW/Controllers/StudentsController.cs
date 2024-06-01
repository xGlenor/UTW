using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLibrary.Contracts;
using BaseLibrary.GenericModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Models;
using BaseLibrary.Responses;

namespace ServerUTW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repository;

        public StudentsController(IStudentRepository context)
        {
            _repository = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _repository.GetAll();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _repository.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        
        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            var studentAdded = await _repository.Insert(student);

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        [HttpPut("{id:int}")]
        public async Task<EntityResponse> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return new EntityResponse(false, "Bad ID", null);
            }

            var result = await _repository.Update(id, student);

            return new EntityResponse(true, "Updated successfully", Generics.SerializeObj(result));
        }
        
        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _repository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentDeleted = await _repository.Delete(student.Id);
            
            return Ok(new {Message = "Pomyślnie usunięto użytkownika", Student = studentDeleted});
        }
        
    }
}

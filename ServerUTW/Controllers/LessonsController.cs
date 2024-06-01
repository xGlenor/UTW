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
using BaseLibrary.GenericModels;
using BaseLibrary.Responses;

namespace ServerUTW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonRepository _repository;

        public LessonsController(ILessonRepository context)
        {
            _repository = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _repository.GetAll();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            var lesson = await _repository.GetById(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }

        
        [HttpPut("{id:int}")]
        public async Task<EntityResponse> PutStudent(int id, Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return new EntityResponse(false, "Bad ID", null);
            }

            var result = await _repository.Update(id, lesson);

            return new EntityResponse(true, "Updated successfully", Generics.SerializeObj(result));
        }

        
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        {
           var lessonAdded = await _repository.Insert(lesson);

            return CreatedAtAction("GetLesson", new { id = lesson.Id }, lesson);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lesson = await _repository.GetById(id);
            if (lesson == null)
            {
                return NotFound();
            }

            var lessonDeleted = await _repository.Delete(lesson.Id);

            return Ok(new { Message = "Pomyślnie usunięto zajecia", Lesson = lessonDeleted });
        }

        
    }
}

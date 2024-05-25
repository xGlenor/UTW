using BaseLibrary.Contracts;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerUTW.Data;
using System.Net;

namespace ServerUTW.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext _dbContext;

    public LessonRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Lesson> Delete(int lessonID)
    {
        var lesson = await _dbContext.Lessons.FirstOrDefaultAsync(lesson => lesson.Id.Equals(lessonID));

        if (lesson == null)
            return null;

        _dbContext.Lessons.Remove(lesson);
        await _dbContext.SaveChangesAsync();
        return lesson;
    }

    public async Task<List<Lesson>> GetAll()
    {
        return await _dbContext.Lessons.ToListAsync();
    }

    public async Task<Lesson?> GetById(int lessonID)
    {
        return await _dbContext.Lessons.FindAsync(lessonID);
    }

    public async Task<Lesson> Insert(Lesson lesson)
    {
        await _dbContext.Lessons.AddAsync(lesson);
        await _dbContext.SaveChangesAsync();
        return lesson;
    }

    public async Task<Lesson> Update(int id, Lesson lesson)
    {
        var existingLesson = await _dbContext.Lessons.FirstOrDefaultAsync(lesson => lesson.Id.Equals(id));

        if (existingLesson == null)
            return null;

        existingLesson.Name = lesson.Name;
        existingLesson.Description = lesson.Description;
        existingLesson.LessonDate = lesson.LessonDate;
        existingLesson.Classroom = lesson.Classroom;
        existingLesson.Address = lesson.Address;
        existingLesson.NumberOfPlaces = lesson.NumberOfPlaces;
        existingLesson.Price = lesson.Price;
        existingLesson.Teachers = lesson.Teachers;

        await _dbContext.SaveChangesAsync();
        return existingLesson;
    }
}
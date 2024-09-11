using BaseLibrary.Contracts;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerUTW.Data;

namespace ServerUTW.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _dbContext;

    public EnrollmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Enrolllment> Delete(int enrollmentID)
    {
        var enrollment =
            await _dbContext.Enrolllments.FirstOrDefaultAsync(enrollment => enrollment.Id.Equals(enrollmentID));

        if (enrollment == null)
            return null;

        _dbContext.Enrolllments.Remove(enrollment);
        await _dbContext.SaveChangesAsync();
        return enrollment;
    }

    public async Task<List<Enrolllment>> GetAll()
    {
        return await _dbContext.Enrolllments
            .Include(s => s.Student)
            .Include(l => l.Lesson)
            .Include(l => l.Lesson.Session)
            .ToListAsync();
    }

    public async Task<Enrolllment?> GetById(int enrollmentID)
    {
        return await _dbContext.Enrolllments.FindAsync(enrollmentID);
    }

    public async Task<Enrolllment> Insert(Enrolllment enrollment)
    {
        await _dbContext.Enrolllments.AddAsync(enrollment);
        await _dbContext.SaveChangesAsync();
        return enrollment;
    }


    public async Task<Enrolllment> Update(int id, Enrolllment enrolllment)
    {
        var existingEnrollment =
            await _dbContext.Enrolllments.FirstOrDefaultAsync(enrolllment => enrolllment.Id.Equals(id));

        if (existingEnrollment == null)
            return null;

        existingEnrollment.StudentId = enrolllment.StudentId;
        existingEnrollment.LessonId = enrolllment.LessonId;
        existingEnrollment.Student = enrolllment.Student;
        existingEnrollment.Lesson = enrolllment.Lesson;

        await _dbContext.SaveChangesAsync();
        return existingEnrollment;
    }

    public async Task<List<TeacherEnrollment>> GetAllTeachers()
    {
        return await _dbContext.TeacherEnrollments
            .Include(s => s.Teacher)
            .Include(l => l.Lesson)
            .Include(l => l.Lesson.Session)
            .ToListAsync();
    }

    public async Task<TeacherEnrollment?> GetTeacherById(int enrollmentID)
    {
        return await _dbContext.TeacherEnrollments.FindAsync(enrollmentID);
    }

    public async Task<TeacherEnrollment> InsertTeacher(TeacherEnrollment enrollment)
    {
        await _dbContext.TeacherEnrollments.AddAsync(enrollment);
        await _dbContext.SaveChangesAsync();
        return enrollment;
    }

    public async Task<TeacherEnrollment> UpdateTeacher(int id, TeacherEnrollment enrollment)
    {
        var existingEnrollment =
            await _dbContext.TeacherEnrollments.FirstOrDefaultAsync(enrolllment => enrolllment.Id.Equals(id));

        if (existingEnrollment == null)
            return null;

        existingEnrollment.TeacherId = enrollment.TeacherId;
        existingEnrollment.LessonId = enrollment.LessonId;
        existingEnrollment.Teacher = enrollment.Teacher;
        existingEnrollment.Lesson = enrollment.Lesson;

        await _dbContext.SaveChangesAsync();
        return existingEnrollment;
    }

    public async Task<TeacherEnrollment> DeleteTeacher(int enrollmentID)
    {
        var enrollment =
            await _dbContext.TeacherEnrollments.FirstOrDefaultAsync(enrollment => enrollment.Id.Equals(enrollmentID));

        if (enrollment == null)
            return null;

        _dbContext.TeacherEnrollments.Remove(enrollment);
        await _dbContext.SaveChangesAsync();
        return enrollment;
    }

    public async Task<List<TeacherEnrollment>> GetTeacherEnrollmentsById(string userId)
    {
        return await _dbContext.TeacherEnrollments
            .Where(te => te.TeacherId.Equals(userId))
            .Include(s => s.Teacher)
            .Include(l => l.Lesson)
            .ToListAsync();
    }

    public async Task<List<Enrolllment>> GetStudentbyLessonId(int LessonId)
    {
        return await _dbContext.Enrolllments
            .Where(te => te.LessonId.Equals(LessonId))
            .Include(s => s.Student)
            .Include(l => l.Lesson)
            .ToListAsync();
    }
}
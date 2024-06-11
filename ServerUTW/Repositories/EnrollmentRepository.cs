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
        var enrollment = await _dbContext.Enrolllments.FirstOrDefaultAsync(enrollment => enrollment.Id.Equals(enrollmentID));

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
        var existingEnrollment = await _dbContext.Enrolllments.FirstOrDefaultAsync(enrolllment => enrolllment.Id.Equals(id));

        if (existingEnrollment == null)
            return null;

        existingEnrollment.StudentId = enrolllment.StudentId;
        existingEnrollment.LessonId = enrolllment.LessonId;
        existingEnrollment.Student = enrolllment.Student;
        existingEnrollment.Lesson = enrolllment.Lesson;

        await _dbContext.SaveChangesAsync();
        return existingEnrollment;
    }
}
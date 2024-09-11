using BaseLibrary.Contracts;
using BaseLibrary.Models;
using Microsoft.EntityFrameworkCore;
using ServerUTW.Data;

namespace ServerUTW.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _dbContext;

    public StudentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Student>> GetAll()
    {
        return await _dbContext.Students
            .Include(e => e.Enrolllments)
            .ToListAsync();
    }

    public async Task<List<Student>> GetStudents()
    {
        return await _dbContext.Students
            .Include(e => e.Enrolllments)
            .Where(s => s.IsEnrolled.Equals(true))
            .ToListAsync();
    }

    public async Task<List<Student>> GetCandidates()
    {
        return await _dbContext.Students
            .Where(s => s.IsEnrolled.Equals(false))
            .Include(e => e.Enrolllments)
            .ToListAsync();
    }

    public async Task<Student?> GetById(int studentId)
    {
        return await _dbContext.Students.FindAsync(studentId);
    }

    public async Task<Student> Insert(Student student)
    {
        var addedStudent = await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();
        return addedStudent.Entity;
    }

    public async Task<Student> Update(int id, Student student)
    {
        var existingStudent = await _dbContext.Students.FirstOrDefaultAsync(student1 => student1.Id.Equals(id));

        if (existingStudent == null)
            return null!;

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Address = student.Address;
        existingStudent.Birthdate = student.Birthdate;
        existingStudent.IsEnrolled = student.IsEnrolled;

        await _dbContext.SaveChangesAsync();
        return existingStudent;
    }

    public async Task<Student> Delete(int studentId)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(student => student.Id.Equals(studentId));

        if (student == null)
            return null!;

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();
        return student;
    }
}
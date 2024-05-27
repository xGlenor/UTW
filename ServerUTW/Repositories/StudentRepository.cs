using BaseLibrary.Contracts;
using BaseLibrary.Models;
using BaseLibrary.Responses;
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
        return await _dbContext.Students.ToListAsync();
    }

    public async Task<Student?> GetById(int studentID)
    {
        return await _dbContext.Students.FindAsync(studentID);
    }

    public async Task<Student> Insert(Student student)
    {
        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();
        return student;
    }

    public async Task<Student> Update(int id, Student student)
    {
        var existingStudent = await _dbContext.Students.FirstOrDefaultAsync(student => student.Id.Equals(id));

        if (existingStudent == null)
            return null;

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Address = student.Address;
        existingStudent.Birthdate = student.Birthdate;

        await _dbContext.SaveChangesAsync();
        return existingStudent;
    }

    public async Task<Student> Delete(int studentID)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(student => student.Id.Equals(studentID));

        if (student == null)
            return null;

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();
        return student;
    }
}
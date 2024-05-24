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

    public Task<Student[]> GetStudents()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Student>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Student> GetById(int studentID)
    {
        throw new NotImplementedException();
    }

    public Task<Student> Insert(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<Student> Update(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<Student> Delete(int studentID)
    {
        throw new NotImplementedException();
    }
}
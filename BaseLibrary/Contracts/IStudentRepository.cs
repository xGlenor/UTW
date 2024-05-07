using BaseLibrary.Models;

namespace BaseLibrary.Contracts;

public interface IStudentRepository
{
    Task<Student[]> GetStudents();
}
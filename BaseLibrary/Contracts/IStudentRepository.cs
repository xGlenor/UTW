using BaseLibrary.Models;
using BaseLibrary.Responses;

namespace BaseLibrary.Contracts;

public interface IStudentRepository
{
    Task<Student[]> GetStudents();
    Task<IEnumerable<Student>> GetAll();
    Task<Student> GetById(int studentID);
    Task<Student> Insert(Student student);
    Task<Student> Update(Student student);
    Task<Student> Delete(int studentID);
}
using BaseLibrary.Models;

namespace BaseLibrary.Contracts;

public interface IStudentRepository
{
    Task<Student[]> GetStudents();
    IEnumerable<Student> GetAll();
    Student GetById(int StudentID);
    void Insert(Student student);
    void Update(Student student);
    void Delete(int StudentnID);
    void Save();
}
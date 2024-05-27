using BaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Contracts
{
public interface IStudentRepository
{
    Task<List<Student>> GetAll();
    Task<Student?> GetById(int studentID);
    Task<Student> Insert(Student student);
    Task<Student> Update(int id, Student student);
    Task<Student> Delete(int studentID);
}

}
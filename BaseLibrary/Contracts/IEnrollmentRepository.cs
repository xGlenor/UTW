using BaseLibrary.Models;
using System.Runtime.InteropServices;

namespace BaseLibrary.Contracts;

public interface IEnrollmentRepository
{
        Task<List<Enrolllment>> GetAll();
        Task<Enrolllment?> GetById(int enrollmentID);
        Task<Enrolllment> Insert(Enrolllment enrollment);
        Task<Enrolllment> Update(int id, Enrolllment enrollment);
        Task<Enrolllment> Delete(int enrollmentID);
        
        Task<List<TeacherEnrollment>> GetAllTeachers();
        Task<TeacherEnrollment?> GetTeacherById(int enrollmentID);
        Task<TeacherEnrollment> InsertTeacher(TeacherEnrollment enrollment);
        Task<TeacherEnrollment> UpdateTeacher(int id, TeacherEnrollment enrollment);
        Task<TeacherEnrollment> DeleteTeacher(int enrollmentID);

        Task<List<TeacherEnrollment>> GetTeacherEnrollmentsById(string userId);

        Task<List<Enrolllment>> GetStudentbyLessonId(int LessonId);

}
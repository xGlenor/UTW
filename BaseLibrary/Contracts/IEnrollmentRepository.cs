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

}
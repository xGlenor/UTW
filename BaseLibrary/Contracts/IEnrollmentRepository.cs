using BaseLibrary.Models;
using System.Runtime.InteropServices;

namespace BaseLibrary.Contracts;

public interface IEnrollmentRepository
{
        IEnumerable<Enrolllment> GetAll();
        Enrolllment GetById(int EnrollmentID);
        void Insert(Enrolllment enrolllment);
        void Update(Enrolllment enrolllment);
        void Delete(int EnrollmentID);
        void Save();
    
}
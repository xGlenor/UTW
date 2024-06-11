using BaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Contracts
{
    public interface ILessonRepository
    {
        Task<List<Lesson>> GetAll();
        Task<List<Lesson>> GetBySessionId(int sessionId);
        Task<Lesson?> GetById(int lessonID);
        Task<Lesson> Insert(Lesson lesson);
        Task<Lesson> Update(int id, Lesson lesson);
        Task<Lesson> Delete(int lessonID);
    }
}


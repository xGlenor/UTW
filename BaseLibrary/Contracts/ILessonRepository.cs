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
        IEnumerable<Lesson> GetAll();
        Lesson GetById(int LessonID);
        void Insert(Lesson lesson);
        void Update(Lesson lesson);
        void Delete(int LessonID);
        void Save();
    }
}

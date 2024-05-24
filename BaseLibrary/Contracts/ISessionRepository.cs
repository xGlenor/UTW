using BaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Contracts
{
    public interface ISessionRepository
    {
        IEnumerable<Session> GetAll();
        Session GetById(int SessionID);
        void Insert(Session session);
        void Update(Session session);
        void Delete(int SessionID);
        void Save();
    }
}

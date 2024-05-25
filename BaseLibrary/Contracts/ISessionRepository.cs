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
        Task<List<Session>> GetAll();
        Task<Session?> GetById(int sessionID);
        Task<Session> Insert(Session session);
        Task<Session> Update(int id, Session session);
        Task<Session> Delete(int sessionID);
    }
}

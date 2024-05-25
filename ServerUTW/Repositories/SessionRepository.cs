using BaseLibrary.Contracts;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerUTW.Data;

namespace ServerUTW.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _dbContext;

    public SessionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> Delete(int sessionID)
    {
        var session = await _dbContext.Sessions.FirstOrDefaultAsync(session => session.Id.Equals(sessionID));

        if (session == null)
            return null;

        _dbContext.Sessions.Remove(session);
        await _dbContext.SaveChangesAsync();
        return session;
    }

    public async Task<List<Session>> GetAll()
    {
        return await _dbContext.Sessions.ToListAsync();
    }

    public async Task<Session?> GetById(int sessionID)
    {
        return await _dbContext.Sessions.FindAsync(sessionID);
    }

    public async Task<Session> Insert(Session session)
    {
        await _dbContext.Sessions.AddAsync(session);
        await _dbContext.SaveChangesAsync();
        return session;
    }

    public async Task<Session> Update(int id, Session session)
    {
        var existingSession = await _dbContext.Sessions.FirstOrDefaultAsync(session => session.Id.Equals(id));

        if (existingSession == null)
            return null;

        existingSession.SessionYear = session.SessionYear;
        existingSession.SessionType = session.SessionType;
        existingSession.Students = session.Students;
        existingSession.Lessons = session.Lessons;

        await _dbContext.SaveChangesAsync();
        return existingSession;
    }
}
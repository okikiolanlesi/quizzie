using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzie.Data;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class QuizSessionRepository : IQuizSessionRepository
{
    private readonly QuizzieDbContext _context;

    public QuizSessionRepository(QuizzieDbContext context)
    {
        _context = context;
    }
    public void Add(QuizSession quizSession)
    {
        _context.QuizSessions.Add(quizSession);
    }

    public async Task<List<QuizSession>> GetAllForAUser(Guid userId)
    {
        return await _context.QuizSessions.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<QuizSession> GetById(Guid id)
    {
        return await _context.QuizSessions.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    public void MarkAsModified(QuizSession quizSession)
    {
        _context.Entry(quizSession).State = EntityState.Modified;
    }

    public async Task<QuizSession> GetOngoingQuizForUser(Guid userId, Guid quizId)
    {
        var ongoingSession = await _context.QuizSessions.FirstOrDefaultAsync(x => x.EndTime > DateTime.UtcNow && !x.IsCompleted && x.QuizId == quizId);
        return ongoingSession;
    }
}


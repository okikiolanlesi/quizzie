using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Quizzie.Data;
using Quizzie.DTOs;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class QuizSessionRepository : IQuizSessionRepository
{
    private readonly QuizzieDbContext _context;
    private readonly IMapper _mapper;

    public QuizSessionRepository(QuizzieDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void Add(QuizSession quizSession)
    {
        _context.QuizSessions.Add(quizSession);
    }

    public async Task<List<QuizSession>> GetAllForAUser(Guid userId)
    {
        return await _context.QuizSessions.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<QuizSessionDto> GetById(Guid id)
    {
        return await _context.QuizSessions.Include(x => x.UserAnswers).Include(x => x.Quiz).ProjectTo<QuizSessionDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
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
        var ongoingSession = await _context.QuizSessions.FirstOrDefaultAsync(x => x.EndTime > DateTime.UtcNow && !x.IsCompleted && x.QuizId == quizId && x.UserId == userId);
        return ongoingSession;
    }

    public async Task<int> CountCorrectAnswers(Guid quizSessionId)
    {
        return await _context.Answers.CountAsync(x => x.QuizSessionId == quizSessionId && x.Option.IsCorrect);
    }

    public async Task<int> CountTotalQuestions(Guid quizId)
    {
        return await _context.Questions.CountAsync(x => x.QuizId == quizId);

    }

    public async Task<List<QuizSession>> GetEndedSessionsForAUser(Guid userId)
    {
        return await _context.QuizSessions.Where(x => x.EndTime <= DateTime.UtcNow && !x.IsCompleted).ToListAsync();
    }
}


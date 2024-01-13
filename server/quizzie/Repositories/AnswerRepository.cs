using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzie.Data;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly QuizzieDbContext _context;

    public AnswerRepository(QuizzieDbContext context)
    {
        _context = context;
    }
    public void Add(Answer answer)
    {

        _context.Answers.Add(answer);

    }

    public async Task<List<Answer>> GetAllForAQuizSession(Guid quizSessionId)
    {
        return await _context.Answers.Where(x => x.QuizSessionId == quizSessionId).ToListAsync();
    }

    public async Task<Answer> GetAnswerForQuestion(Guid quizSessionId, Guid questionId)
    {
        return await _context.Answers.FirstOrDefaultAsync(x => x.QuizSessionId == quizSessionId && x.QuestionId == questionId);
    }

    public void MarkAsModified(Answer answer)
    {
        _context.Entry(answer).State = EntityState.Modified;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

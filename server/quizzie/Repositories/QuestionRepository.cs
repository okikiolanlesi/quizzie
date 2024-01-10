using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Quizzie.Data;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly QuizzieDbContext _context;

    public QuestionRepository(QuizzieDbContext context)
    {
        _context = context;
    }
    public void Add(Question question)
    {
        _context.Questions.Add(question);
    }

    public Task<List<Question>> GetAllForAQuiz(Guid quizId)
    {
        throw new NotImplementedException();
    }

    public Task<Question> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void MarkAsModified(Question question)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }
}

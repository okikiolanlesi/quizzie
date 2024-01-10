using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.Data;
using Quizzie.Models;

namespace Quizzie.Repositories;

public class OptionRepository : IOptionRepository
{
    private readonly QuizzieDbContext _context;

    public OptionRepository(QuizzieDbContext context)
    {
        _context = context;
    }
    public void Add(Option Option)
    {
        throw new NotImplementedException();
    }

    public void AddRange(List<Option> options)
    {
        _context.Options.AddRange(options);
    }

    public Task<List<Option>> GetAllForAQuestion(Guid questionId)
    {
        throw new NotImplementedException();
    }

    public Task<Option> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void MarkAsModified(Option Option)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

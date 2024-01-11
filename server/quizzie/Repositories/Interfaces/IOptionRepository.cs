using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzie.Models;

namespace Quizzie.Repositories;

public interface IOptionRepository
{
    Task<Option> GetById(Guid id);
    Task<List<Option>> GetAllForAQuestion(Guid questionId);

    void Add(Option Option);
    Task<Option> AddOption(Option option);
    void AddRange(List<Option> Option);
    Task<bool> SaveChangesAsync();

    void MarkAsModified(Option Option);
}
